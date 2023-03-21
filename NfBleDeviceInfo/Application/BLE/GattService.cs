using System;
using System.Diagnostics;

using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;
using nanoFramework.Runtime.Native;


namespace NfBleDeviceInfo.Application.BLE
{
	internal class GattService
	{
		private readonly GattServiceProvider _applicationServiceProvider;

		internal GattService()
		{
			// The GattServiceProvider is used to create and advertise the primary service definition.
			// An extra device information service will be automatically created.
			var serviceProviderResult = GattServiceProvider.Create(UuIds.ConfigServiceId.GetUuid());
			if (serviceProviderResult.Error != BluetoothError.Success)
				return;  // ToDo some kind of error state - blink LED

			_applicationServiceProvider = serviceProviderResult.ServiceProvider;
			// Get created service from provider
			ApplicationService = _applicationServiceProvider.Service; // needed before changing device info?

			DeviceInformationServiceService _ = new("TradeCraftInc", "motoAppAratus", "x001", new Version(0, 0, 0, 0).ToString(), SystemInfo.Version.ToString(), new Version(0, 0, 0, 0).ToString());
		}

		internal GattLocalService ApplicationService { get; }

		internal void Advertise()
		{
			_applicationServiceProvider.StartAdvertising(new GattServiceProviderAdvertisingParameters
			{
				IsConnectable = true,
				IsDiscoverable = true,
				//ServiceData = new(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 })
			});

			#if DEBUG
			if (_applicationServiceProvider.AdvertisementStatus == GattServiceProviderAdvertisementStatus.StartedWithoutAllAdvertisementData)
				Debug.WriteLine($"advert status: {_applicationServiceProvider.AdvertisementStatus}");
			#else
			#endif
		}
	}
}
