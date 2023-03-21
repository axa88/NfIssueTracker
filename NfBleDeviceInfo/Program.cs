using System;
using System.Threading;

using NfBleDeviceInfo.Application.BLE;


namespace NfBleDeviceInfo
{
	public class Program
	{
		public static void Main()
		{
			Thread.Sleep(TimeSpan.FromSeconds(3));

			var server = new Server();

			GattService gattService = new();

			gattService.Advertise();

			Thread.Sleep(Timeout.Infinite);
		}
	}
}
