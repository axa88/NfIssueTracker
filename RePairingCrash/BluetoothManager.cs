using System;
using System.Diagnostics;

using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.Advertisement;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

using Peripheral.Utility.BT;


namespace RePairingCrash;

internal class BluetoothManager
{
	private readonly BluetoothLEServer _server = BluetoothLEServer.Instance;
	private GattServiceProvider _primaryProvider;
	private GattServiceProviderAdvertisingParameters _advertisingParams;

	public void Start()
	{
		_server.DeviceName = "nfBLE"; // bug #1722
		//_server.Appearance = (ushort)SubCategory.GenericPhone; // bug #1722
		ConfigurePairing();
		ConfigureSession();
		// _server.Start(); // oddly, also called upon advertising
		ConfigureServices();
		StartAdvertising();
	}

	private void ConfigurePairing()
	{
		PrintConfig(_server.Pairing);

		// security
		_server.Pairing.IOCapabilities = DevicePairingIOCapabilities.NoInputNoOutput;
		_server.Pairing.ProtectionLevel = DevicePairingProtectionLevel.None;

		_server.Pairing.PairingComplete += (devicePairing, args) =>
		{
			Debug.WriteLine($"{nameof(_server.Pairing.PairingComplete)} <<< ==============");
			Debug.WriteLine($"\t{nameof(args.Status)}:{args.Status.ToName()}");
			PrintConfig((DevicePairing)devicePairing);
		};

		// callbacks
		_server.Pairing.PairingRequested += (bluetoothLeServer, pairingArgs) =>
		{
			var server = (BluetoothLEServer)bluetoothLeServer;

			Debug.WriteLine($"============== >>> {nameof(_server.Pairing.PairingRequested)}");
			Debug.WriteLine($"\t{nameof(server.Pairing.IOCapabilities)}: {server.Pairing.IOCapabilities.ToName()}");

			// DevicePairingKinds should have FLAGS attribute, so this check is now pointless. Bug or just not supported in NF?
			// ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
			if (pairingArgs.PairingKind == DevicePairingKinds.None || (pairingArgs.PairingKind & (pairingArgs.PairingKind - 1)) != 0)
			{
				Debug.WriteLine("Remote devices indicates it does not support pairing or has chosen more than one pairing mode?! the latter shouldn't be possible ");
				return;
			}

			Debug.WriteLine($"{nameof(pairingArgs.PairingKind)}:{pairingArgs.PairingKind.ToName()} {nameof(pairingArgs.Pin)}:{pairingArgs.Pin}");

			switch (pairingArgs.PairingKind)
			{
				case DevicePairingKinds.None: break; // this shouldn't be possible and blocked with check above
				case DevicePairingKinds.ConfirmOnly: break;
				case DevicePairingKinds.ConfirmPinMatch: break; // numeric confirmation // DevicePairingIOCapabilities.DisplayYesNo;
				case DevicePairingKinds.ProvidePin: break; // pin provided // DevicePairingIOCapabilities.KeyboardOnly;
				case DevicePairingKinds.DisplayPin: break;
				default: Debug.WriteLine($"Pairing mode not supported: {pairingArgs.PairingKind.ToName()}"); break;
			}
		};

		static void PrintConfig(DevicePairing dp)
		{
			Debug.WriteLine($"{nameof(dp.AllowBonding)}:{dp.AllowBonding}");
			Debug.WriteLine($"{nameof(dp.CanPair)}:{dp.CanPair}");
			Debug.WriteLine($"{nameof(dp.IsPaired)}:{dp.IsPaired}");
			Debug.WriteLine($"{nameof(dp.IsAuthenticated)}:{dp.IsAuthenticated}");
			Debug.WriteLine($"{nameof(dp.WasSecureConnectionUsedForPairing)}:{dp.WasSecureConnectionUsedForPairing}");
			Debug.WriteLine($"{nameof(dp.IOCapabilities)}:{dp.IOCapabilities.ToName()}");
			Debug.WriteLine($"{nameof(dp.ProtectionLevel)}:{dp.ProtectionLevel.ToName()}");
			Debug.WriteLine();
		}
	}

	private void ConfigureSession()
	{
		Debug.WriteLine($"{nameof(_server.Session.DeviceId)} {nameof(_server.Session.DeviceId)}:{_server.Session.DeviceId.Id}");
		Debug.WriteLine($"{nameof(_server.Session.DeviceId)} {nameof(_server.Session.MaxMtuSize)}:{_server.Session.MaxMtuSize}");

		_server.Session.SessionStatusChanged += (gattSession, statusChanged) => // connection
		{
			var gs = (GattSession)gattSession;
			Debug.WriteLine($"{nameof(gs.SessionStatusChanged)}"
							+ $" {nameof(gs.DeviceId.Id)} {gs.DeviceId.Id}"
							+ $" {nameof(statusChanged.Status)}:{statusChanged.Status.ToName()}"
							+ $" {nameof(statusChanged.Error)}:{statusChanged.Error.ToName()}");

			switch (statusChanged.Status)
			{
				case GattSessionStatus.Active: break;
				case GattSessionStatus.Closed: _primaryProvider?.StartAdvertising(); break;
				default: throw new ArgumentOutOfRangeException();
			}

			Debug.WriteLine($"{nameof(_primaryProvider.AdvertisementStatus)}:{_primaryProvider?.AdvertisementStatus.ToName()}");
		};
	}

	private void ConfigureServices()
	{
		var primaryResult = GattServiceProvider.Create(new("DEADFACE-DEAF-DEAD-FACE-DEAFDEADFACE"));
		if (primaryResult.Error != BluetoothError.Success)
			throw new ApplicationException("Failed to create: DEADFACE-DEAF-DEAD-FACE-DEAFDEADFACE");

		_primaryProvider = primaryResult.ServiceProvider;

		Debug.WriteLine($"{nameof(_primaryProvider.AdvertisementStatus)}:{_primaryProvider.AdvertisementStatus.ToName()}");
	}

	public void StartAdvertising()
	{
		_advertisingParams = new()
		{
			IsConnectable = true,
			IsDiscoverable = true,
			CustomAdvertisement = true,
			Advertisement =
			{
				Flags = BluetoothLEAdvertisementFlags.ClassicNotSupported | BluetoothLEAdvertisementFlags.GeneralDiscoverableMode,
				LocalName = "nfAdv"
			},
			ServiceData = new([1, 2, 3, 4])
		};

		_primaryProvider?.StartAdvertising(_advertisingParams);
		Debug.WriteLine($"{nameof(_primaryProvider.AdvertisementStatus)}:{_primaryProvider?.AdvertisementStatus.ToName()}");
	}

	public void StopAdvertising()
	{
		_primaryProvider?.StopAdvertising();
		Debug.WriteLine($"{nameof(_primaryProvider.AdvertisementStatus)}:{_primaryProvider?.AdvertisementStatus.ToName()}");
	}
}
