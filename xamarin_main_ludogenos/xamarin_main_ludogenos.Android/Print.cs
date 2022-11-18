using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using XF.Bluetooth.Printer.Plugin.Abstractions;

using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;

namespace SunmiDemo.Droid
{
    public class Print : IPrint
    {
        public async Task PrintText(string input, string printerName)
        {
            using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
            {
                if (bluetoothAdapter == null)
                {
                    throw new Exception("No default adapter");
                    //return;
                }

                if (!bluetoothAdapter.IsEnabled)
                {
                    throw new Exception("Bluetooth not enabled");
                }

                BluetoothDevice device = (from bd in bluetoothAdapter.BondedDevices
                                          where bd.Name == printerName
                                          select bd).FirstOrDefault();
                if (device == null)
                    throw new Exception(printerName + " device not found.");

                try
                {
                    using (BluetoothSocket _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                    {

                        await _socket.ConnectAsync();
                        

                        switch (input)
                        {
                            case "LF":
                                _socket.OutputStream.WriteByte(0x0A);
                                break;

                            case "BR":
                                var a = new EPSON();
                                var buf = ByteSplicer.Combine(
                                    a.CenterAlign(),
                                    a.SetBarcodeHeightInDots(48), a.PrintBarcode(BarcodeType.JAN13_EAN13, "123456789012"), a.PrintLine("")
                                    );
                                await _socket.OutputStream.WriteAsync(buf, 0, buf.Length);
                                break;

                            default:
                                byte[] message = System.Text.Encoding.ASCII.GetBytes(input);
                                await _socket.OutputStream.WriteAsync(message, 0, message.Length);
                                _socket.OutputStream.WriteByte(0x0A);

                                //try ESCPOS_NET
                                var e = new EPSON();
                                var buffer = ByteSplicer.Combine(
                                    e.CenterAlign(), 
                                    e.PrintLine("Receipt"),
                                    e.Print("From "), e.SetStyles(PrintStyle.Bold), e.Print($"bold"), e.SetStyles(PrintStyle.None),
                                    e.SetBarcodeHeightInDots(48), e.PrintBarcode(BarcodeType.CODE39, "ABC"), e.PrintLine(""),
                                    e.SetBarcodeHeightInDots(48), e.PrintQRCode(printerName="google.com"), e.PrintLine(""),
                                    e.LeftAlign(), e.PrintLine("left"),
                                    e.PrintLine("user: " + "username")
                                    );
                                await _socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                                break;
                        }
                        
                        _socket.Close();
                    }
                }
                catch (Exception exp)
                {

                    throw exp;
                }


            }
        }
    }
}