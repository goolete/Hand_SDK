using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hand
{
    public class HandUntil
    {
        private static BluetoothClient bluetoothClient;
        private static BluetoothComponent bluetoothComponent;
        private static Stream peerStream;

        public delegate void Del_BluetoothComponent_DiscoverDevicesProgress(object sender, DiscoverDevicesEventArgs e);

        public static Byte[] send_data;

        /// <summary>
        /// 初始化蓝牙
        /// </summary>
        public static void HandInit()
        {
            bluetoothClient = new BluetoothClient();
            BluetoothRadio bluetoothRadio = BluetoothRadio.PrimaryRadio;
            bluetoothRadio.Mode = RadioMode.Connectable;
        }

        /// <summary>
        /// 扫描蓝牙设备并回调
        /// </summary>
        /// <param name="del_BluetoothComponent_DiscoverDevicesProgress">扫描到一个设备的回调函数</param>
        public static void HandScan(
            Del_BluetoothComponent_DiscoverDevicesProgress del_BluetoothComponent_DiscoverDevicesProgress)
        {
            bluetoothComponent = new BluetoothComponent(bluetoothClient);
            // 开始异步扫描
            bluetoothComponent.DiscoverDevicesAsync(20, false, false, false, true, bluetoothComponent);
            // 扫描结果回调
            EventHandler<DiscoverDevicesEventArgs> handler = new EventHandler<DiscoverDevicesEventArgs>(del_BluetoothComponent_DiscoverDevicesProgress);
            bluetoothComponent.DiscoverDevicesProgress += handler;
        }

        /// <summary>
        /// 连接机械手
        /// </summary>
        /// <param name="blueAddress">机械手蓝牙地址</param>
        public static void HandConnect(BluetoothAddress blueAddress)
        {
            BluetoothEndPoint ep = new BluetoothEndPoint(blueAddress, BluetoothService.SerialPort);
            bluetoothClient.Connect(ep);

            if (bluetoothClient.Connected)
            {
                peerStream = bluetoothClient.GetStream();     //创建IO流对象
            }
        }

        /// <summary>
        /// 断开机械手连接
        /// </summary>
        public static void HandDisconeect()
        {
            if (bluetoothClient != null)
            {
                bluetoothClient.Close();
            }
        }

        /// <summary>
        /// 发送控制指令
        /// </summary>
        /// <param name="send_data"></param>
        public static void HandSendByte(Byte[] send_data)
        {
            peerStream.Write(send_data, 0, 8);
        }

        /// <summary>
        /// 发送控制指令
        /// </summary>
        /// <param name="finger"></param>
        public static void HandSend(byte[] finger)
        {
            send_data = new Byte[8] { 0xAA, 0xBB, 1, 1, 1, 1, 1, 0xCC };
            Array.Copy(finger, 0, send_data, 2, 5);
            peerStream.Write(send_data, 0, 8);
        }
    }
}
