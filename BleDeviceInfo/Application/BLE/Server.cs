using nanoFramework.Device.Bluetooth;


namespace BleDeviceInfo.Application.BLE
{
	internal class Server
	{
		internal Server()
		{
			// create server instance from singleton, needed to name device
			var server = BluetoothLEServer.Instance;
			server.DeviceName = "motoAppAratus";
		}
	}
}
