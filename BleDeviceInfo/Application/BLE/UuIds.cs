using System;


namespace BleDeviceInfo.Application.BLE
{
	internal static class UuIds
	{
		internal const ushort ConfigServiceId = 0xe000;

		internal const ushort RootMenu = 0xe010;
		internal const ushort SecurityPinId = 0xe020;
		internal const ushort IgnitionConfigId = 0xe030;
		internal const ushort IgnitionComboId = 0xe031;
		internal const ushort IndicatorConfigId = 0xe040;
		internal const ushort IndicatorServiceId = 0xe041;
		internal const ushort LampConfigId = 0xe050;
		internal const ushort BrakeConfigId = 0xe060;

		internal const ushort SecurityMenu = 0xe500;

		internal const ushort IgnitionMenu = 0xe510;
		internal const ushort IgnitionStateMenu = 0xe511;
		internal const ushort IgnitionModeMenu = 0xe512;
		internal const ushort IgnitionGraceMenu = 0xe513;
		internal const ushort IgnitionComboMenu = 0xe514;

		internal const ushort IndicatorMenu = 0xe520;
		internal const ushort IndicatorStateMenu = 0xe521;
		internal const ushort IndicatorButtonMenu = 0xe522;
		internal const ushort IndicatorRateMenu = 0xe523;
		internal const ushort IndicatorOffModeMenu = 0xe524;
		internal const ushort IndicatorOffDelayMenu = 0xe525;
		internal const ushort IndicatorOffAngleMenu = 0xe525;

		internal const ushort LampMenu = 0xe530;

		internal const ushort BrakeMenu = 0xe540;


		internal static Guid GetUuid(this ushort id) => new($"{"deadface-deaf-dead-face-decafcaf"}{id:x4}");
	}
}
