using System;
using System.Diagnostics;
using System.Threading;

using RePairingCrash;


Debug.WriteLine("Hello from nanoFramework!");
Thread.Sleep(TimeSpan.FromSeconds(3)); // help recover from a crash


var btManager = new BluetoothManager();
btManager.Start();

Thread.Sleep(Timeout.Infinite);