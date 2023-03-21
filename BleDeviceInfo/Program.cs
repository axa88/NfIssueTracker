using System;
using System.Threading;

using BleDeviceInfo.Application.BLE;

using nanoFramework.Device.Bluetooth.GenericAttributeProfile;


namespace BleDeviceInfo
{
	public class Program
	{
		public static void Main()
		{
			Thread.Sleep(TimeSpan.FromSeconds(3));

			var server = new Server();

			GattService gattService = new();
			gattService.ApplicationService.CreateCharacteristic(Guid.NewGuid(), new GattLocalCharacteristicParameters());

			gattService.Advertise();

			Thread.Sleep(Timeout.Infinite);
		}
	}
}
