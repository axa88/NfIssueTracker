using System;

using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;


namespace Peripheral.Utility.BT
{
	internal static class EnumExtensions
	{
		public static string ToName(this GattSessionStatus status)
			=> status switch
			{
				GattSessionStatus.Active => nameof(GattSessionStatus.Active),
				GattSessionStatus.Closed => nameof(GattSessionStatus.Closed),
				_ => "undefined"
			};


		public static string ToName(this BluetoothError error)
			=> error switch
			{
				BluetoothError.Success => nameof(BluetoothError.Success),
				BluetoothError.RadioNotAvailable => nameof(BluetoothError.RadioNotAvailable),
				BluetoothError.ResourceInUse => nameof(BluetoothError.ResourceInUse),
				BluetoothError.DeviceNotConnected => nameof(BluetoothError.DeviceNotConnected),
				BluetoothError.OtherError => nameof(BluetoothError.OtherError),
				BluetoothError.DisabledByPolicy => nameof(BluetoothError.DisabledByPolicy),
				BluetoothError.NotSupported => nameof(BluetoothError.NotSupported),
				BluetoothError.DisabledByUser => nameof(BluetoothError.DisabledByUser),
				BluetoothError.ConsentRequired => nameof(BluetoothError.ConsentRequired),
				BluetoothError.TransportNotSupported => nameof(BluetoothError.TransportNotSupported),
				_ => "undefined"
			};


		public static string ToName(this DevicePairingResultStatus status)
			=> status switch
			{
				DevicePairingResultStatus.Paired => nameof(DevicePairingResultStatus.Paired),
				DevicePairingResultStatus.NotReadyToPair => nameof(DevicePairingResultStatus.NotReadyToPair),
				DevicePairingResultStatus.NotPaired => nameof(DevicePairingResultStatus.NotPaired),
				DevicePairingResultStatus.AlreadyPaired => nameof(DevicePairingResultStatus.AlreadyPaired),
				DevicePairingResultStatus.ConnectionRejected => nameof(DevicePairingResultStatus.ConnectionRejected),
				DevicePairingResultStatus.TooManyConnections => nameof(DevicePairingResultStatus.TooManyConnections),
				DevicePairingResultStatus.HardwareFailure => nameof(DevicePairingResultStatus.HardwareFailure),
				DevicePairingResultStatus.AuthenticationTimeout => nameof(DevicePairingResultStatus.AuthenticationTimeout),
				DevicePairingResultStatus.AuthenticationNotAllowed => nameof(DevicePairingResultStatus.AuthenticationNotAllowed),
				DevicePairingResultStatus.AuthenticationFailure => nameof(DevicePairingResultStatus.AuthenticationFailure),
				DevicePairingResultStatus.NoSupportedProfiles => nameof(DevicePairingResultStatus.NoSupportedProfiles),
				DevicePairingResultStatus.ProtectionLevelCouldNotBeMet => nameof(DevicePairingResultStatus.ProtectionLevelCouldNotBeMet),
				DevicePairingResultStatus.AccessDenied => nameof(DevicePairingResultStatus.AccessDenied),
				DevicePairingResultStatus.InvalidCeremonyData => nameof(DevicePairingResultStatus.InvalidCeremonyData),
				DevicePairingResultStatus.PairingCanceled => nameof(DevicePairingResultStatus.PairingCanceled),
				DevicePairingResultStatus.OperationAlreadyInProgress => nameof(DevicePairingResultStatus.OperationAlreadyInProgress),
				DevicePairingResultStatus.RequiredHandlerNotRegistered => nameof(DevicePairingResultStatus.RequiredHandlerNotRegistered),
				DevicePairingResultStatus.RejectedByHandler => nameof(DevicePairingResultStatus.RejectedByHandler),
				DevicePairingResultStatus.RemoteDeviceHasAssociation => nameof(DevicePairingResultStatus.RemoteDeviceHasAssociation),
				DevicePairingResultStatus.Failed => nameof(DevicePairingResultStatus.Failed),
				_ => "undefined"
			};


		public static string ToName(this DevicePairingIOCapabilities capabilities)
			=> capabilities switch
			{
				DevicePairingIOCapabilities.DisplayOnly => nameof(DevicePairingIOCapabilities.DisplayOnly),
				DevicePairingIOCapabilities.DisplayYesNo => nameof(DevicePairingIOCapabilities.DisplayYesNo),
				DevicePairingIOCapabilities.KeyboardOnly => nameof(DevicePairingIOCapabilities.KeyboardOnly),
				DevicePairingIOCapabilities.NoInputNoOutput => nameof(DevicePairingIOCapabilities.NoInputNoOutput),
				DevicePairingIOCapabilities.KeyboardDisplay => nameof(DevicePairingIOCapabilities.KeyboardDisplay),
				_ => "undefined"
			};


		public static string ToName(this DevicePairingKinds status)
			=> status switch
			{
				DevicePairingKinds.None => nameof(DevicePairingKinds.None),
				DevicePairingKinds.ConfirmOnly => nameof(DevicePairingKinds.ConfirmOnly),
				DevicePairingKinds.DisplayPin => nameof(DevicePairingKinds.DisplayPin),
				DevicePairingKinds.ProvidePin => nameof(DevicePairingKinds.ProvidePin),
				DevicePairingKinds.ConfirmPinMatch => nameof(DevicePairingKinds.ConfirmPinMatch),
				_ => "undefined"
			};


		public static string ToName(this DevicePairingProtectionLevel level)
			=> level switch
			{
				DevicePairingProtectionLevel.Default => nameof(DevicePairingProtectionLevel.Default),
				DevicePairingProtectionLevel.None => nameof(DevicePairingProtectionLevel.None),
				DevicePairingProtectionLevel.Encryption => nameof(DevicePairingProtectionLevel.Encryption),
				DevicePairingProtectionLevel.EncryptionAndAuthentication => nameof(DevicePairingProtectionLevel.EncryptionAndAuthentication),
				_ => "undefined"
			};


		public static string ToName(this GattServiceProviderAdvertisementStatus status)
			=> status switch
			{
				GattServiceProviderAdvertisementStatus.Created => nameof(GattServiceProviderAdvertisementStatus.Created),
				GattServiceProviderAdvertisementStatus.Stopped => nameof(GattServiceProviderAdvertisementStatus.Stopped),
				GattServiceProviderAdvertisementStatus.Started => nameof(GattServiceProviderAdvertisementStatus.Started),
				GattServiceProviderAdvertisementStatus.Aborted => nameof(GattServiceProviderAdvertisementStatus.Aborted),
				GattServiceProviderAdvertisementStatus.StartedWithoutAllAdvertisementData => nameof(GattServiceProviderAdvertisementStatus.StartedWithoutAllAdvertisementData),
				_ => "undefined"
			};

		public static DevicePairingProtectionLevel Next(this DevicePairingProtectionLevel value)
			=> value switch
			{
				DevicePairingProtectionLevel.None => DevicePairingProtectionLevel.Encryption,
				DevicePairingProtectionLevel.Encryption => DevicePairingProtectionLevel.EncryptionAndAuthentication,
				_ => DevicePairingProtectionLevel.None
			};

		public static DevicePairingIOCapabilities Next(this DevicePairingIOCapabilities value)
			=> value switch
			{
				DevicePairingIOCapabilities.NoInputNoOutput => DevicePairingIOCapabilities.DisplayOnly,
				DevicePairingIOCapabilities.DisplayOnly => DevicePairingIOCapabilities.KeyboardOnly,
				DevicePairingIOCapabilities.KeyboardOnly => DevicePairingIOCapabilities.DisplayYesNo,
				DevicePairingIOCapabilities.DisplayYesNo => DevicePairingIOCapabilities.KeyboardDisplay,
				_ => DevicePairingIOCapabilities.NoInputNoOutput
			};
	}
}
