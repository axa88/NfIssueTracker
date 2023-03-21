using System;

using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

using static System.Text.Encoding;


namespace BleDeviceInfo.Application.BLE
{
	/// <summary>
	/// Device Information Service.
	/// </summary>
	public class DeviceInformationServiceService
	{
		readonly GattLocalService _service;

		/// <summary>
		/// Create a new Device Information Service for Provider.
		/// If a manufacture is null the Characteristic will not be included in service.
		/// </summary>
		/// <param name="manufacturer"></param>
		/// <param name="modelNumber"></param>
		/// <param name="serialNumber"></param>
		/// <param name="hardwareRevision"></param>
		/// <param name="firmwareRevision"></param>
		/// <param name="softwareRevision"></param>
		public DeviceInformationServiceService(string manufacturer, string modelNumber = null, string serialNumber = null, string hardwareRevision = null, string firmwareRevision = null, string softwareRevision = null)
		{
			// Add new Device Information Service to provider
			var serviceProviderResult = GattServiceProvider.Create(GattServiceUuids.DeviceInformation);
			if (serviceProviderResult.Error != BluetoothError.Success)
				return; // ToDo Error state blink led

			_service = serviceProviderResult.ServiceProvider.Service;

			CreateReadStaticCharacteristic(GattCharacteristicUuids.ManufacturerNameString, UTF8.GetBytes(manufacturer));
			CreateReadStaticCharacteristic(GattCharacteristicUuids.ModelNumberString, UTF8.GetBytes(modelNumber));
			CreateReadStaticCharacteristic(GattCharacteristicUuids.SerialNumberString, UTF8.GetBytes(serialNumber));
			CreateReadStaticCharacteristic(GattCharacteristicUuids.HardwareRevisionString, UTF8.GetBytes(hardwareRevision));
			CreateReadStaticCharacteristic(GattCharacteristicUuids.FirmwareRevisionString, UTF8.GetBytes(firmwareRevision));
			CreateReadStaticCharacteristic(GattCharacteristicUuids.SoftwareRevisionString, UTF8.GetBytes(softwareRevision));
		}

		private void CreateReadStaticCharacteristic(Guid uuid, byte[] data)
		{
			var writer = new DataWriter();
			writer.WriteBytes(data);
			_service.CreateCharacteristic(uuid, new GattLocalCharacteristicParameters { CharacteristicProperties = GattCharacteristicProperties.Read, StaticValue = writer.DetachBuffer() });
		}}
}
