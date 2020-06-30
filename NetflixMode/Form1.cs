using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Principal;

namespace NetflixMode
{
    public partial class Form1 : Form
    {
        [Flags()]
        enum DisplayModeFlags : int
        {
            Orientation = 0x1,
            PaperSize = 0x2,
            PaperLength = 0x4,
            PaperWidth = 0x8,
            Scale = 0x10,
            Position = 0x20,
            NUP = 0x40,
            DisplayOrientation = 0x80,
            Copies = 0x100,
            DefaultSource = 0x200,
            PrintQuality = 0x400,
            Color = 0x800,
            Duplex = 0x1000,
            YResolution = 0x2000,
            TTOption = 0x4000,
            Collate = 0x8000,
            FormName = 0x10000,
            LogPixels = 0x20000,
            BitsPerPixel = 0x40000,
            PelsWidth = 0x80000,
            PelsHeight = 0x100000,
            DisplayFlags = 0x200000,
            DisplayFrequency = 0x400000,
            ICMMethod = 0x800000,
            ICMIntent = 0x1000000,
            MediaType = 0x2000000,
            DitherType = 0x4000000,
            PanningWidth = 0x8000000,
            PanningHeight = 0x10000000,
            DisplayFixedOutput = 0x20000000
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
        public struct DEVMODE
        {
            public const int CCHDEVICENAME = 32;
            public const int CCHFORMNAME = 32;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            [System.Runtime.InteropServices.FieldOffset(0)]
            public string dmDeviceName;
            [System.Runtime.InteropServices.FieldOffset(32)]
            public Int16 dmSpecVersion;
            [System.Runtime.InteropServices.FieldOffset(34)]
            public Int16 dmDriverVersion;
            [System.Runtime.InteropServices.FieldOffset(36)]
            public Int16 dmSize;
            [System.Runtime.InteropServices.FieldOffset(38)]
            public Int16 dmDriverExtra;
            [System.Runtime.InteropServices.FieldOffset(40)]
            public UInt32 dmFields;

            [System.Runtime.InteropServices.FieldOffset(44)]
            Int16 dmOrientation;
            [System.Runtime.InteropServices.FieldOffset(46)]
            Int16 dmPaperSize;
            [System.Runtime.InteropServices.FieldOffset(48)]
            Int16 dmPaperLength;
            [System.Runtime.InteropServices.FieldOffset(50)]
            Int16 dmPaperWidth;
            [System.Runtime.InteropServices.FieldOffset(52)]
            Int16 dmScale;
            [System.Runtime.InteropServices.FieldOffset(54)]
            Int16 dmCopies;
            [System.Runtime.InteropServices.FieldOffset(56)]
            Int16 dmDefaultSource;
            [System.Runtime.InteropServices.FieldOffset(58)]
            Int16 dmPrintQuality;

            [System.Runtime.InteropServices.FieldOffset(44)]
            public POINTL dmPosition;
            [System.Runtime.InteropServices.FieldOffset(52)]
            public Int32 dmDisplayOrientation;
            [System.Runtime.InteropServices.FieldOffset(56)]
            public Int32 dmDisplayFixedOutput;

            [System.Runtime.InteropServices.FieldOffset(60)]
            public short dmColor; // See note below!
            [System.Runtime.InteropServices.FieldOffset(62)]
            public short dmDuplex; // See note below!
            [System.Runtime.InteropServices.FieldOffset(64)]
            public short dmYResolution;
            [System.Runtime.InteropServices.FieldOffset(66)]
            public short dmTTOption;
            [System.Runtime.InteropServices.FieldOffset(68)]
            public short dmCollate; // See note below!
            [System.Runtime.InteropServices.FieldOffset(72)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;
            [System.Runtime.InteropServices.FieldOffset(102)]
            public Int16 dmLogPixels;
            [System.Runtime.InteropServices.FieldOffset(104)]
            public Int32 dmBitsPerPel;
            [System.Runtime.InteropServices.FieldOffset(108)]
            public Int32 dmPelsWidth;
            [System.Runtime.InteropServices.FieldOffset(112)]
            public Int32 dmPelsHeight;
            [System.Runtime.InteropServices.FieldOffset(116)]
            public Int32 dmDisplayFlags;
            [System.Runtime.InteropServices.FieldOffset(116)]
            public Int32 dmNup;
            [System.Runtime.InteropServices.FieldOffset(120)]
            public Int32 dmDisplayFrequency;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINTL
        {
            public int x;
            public int y;

            public POINTL(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        enum DISP_CHANGE : int
        {
            Successful = 0,
            Restart = 1,
            Failed = -1,
            BadMode = -2,
            NotUpdated = -3,
            BadFlags = -4,
            BadParam = -5,
            BadDualView = -6
        }

        [Flags()]
        public enum ChangeDisplaySettingsFlags : uint
        {
            CDS_NONE = 0,
            CDS_UPDATEREGISTRY = 0x00000001,
            CDS_TEST = 0x00000002,
            CDS_FULLSCREEN = 0x00000004,
            CDS_GLOBAL = 0x00000008,
            CDS_SET_PRIMARY = 0x00000010,
            CDS_VIDEOPARAMETERS = 0x00000020,
            CDS_ENABLE_UNSAFE_MODES = 0x00000100,
            CDS_DISABLE_UNSAFE_MODES = 0x00000200,
            CDS_RESET = 0x40000000,
            CDS_RESET_EX = 0x20000000,
            CDS_NORESET = 0x10000000
        }


        [Flags()]
        enum DisplayDeviceStateFlags : int
        {
            /// <summary>The device is part of the desktop.</summary>
            AttachedToDesktop = 0x1,
            MultiDriver = 0x2,
            /// <summary>The device is part of the desktop.</summary>
            PrimaryDevice = 0x4,
            /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
            MirroringDriver = 0x8,
            /// <summary>The device is VGA compatible.</summary>
            VGACompatible = 0x16,
            /// <summary>The device is removable; it cannot be the primary display.</summary>
            Removable = 0x20,
            /// <summary>The device has more display modes than its output devices support.</summary>
            ModesPruned = 0x8000000,
            Remote = 0x4000000,
            Disconnect = 0x2000000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct DISPLAY_DEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            [MarshalAs(UnmanagedType.U4)]
            public DisplayDeviceStateFlags StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        const int ENUM_CURRENT_SETTINGS = -1;

        public object DevModeFields { get; private set; }
        public bool ShouldClose { get; private set; } = false;

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        [DllImport("user32.dll")]
        static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

        [DllImport("user32.dll")]
        static extern DISP_CHANGE ChangeDisplaySettings(uint lpDevMode, uint dwflags);

        [DllImport("user32.dll")]
        static extern DISP_CHANGE ChangeDisplaySettingsEx(string lpszDeviceName, ref DEVMODE lpDevMode, IntPtr hwnd, ChangeDisplaySettingsFlags dwflags, IntPtr lParam);

        private bool IsInStartup()
        {
            //RegistryKey rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run", false);

            try
            {
                if (rk.GetValue("NetflixMode") != null) {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        private void SetStartup(bool set)
        {
            //RegistryKey rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run", true);

            if (set)
                rk.SetValue("NetflixMode", "\"" + Application.ExecutablePath + "\" -autostart");
            else
                rk.DeleteValue("NetflixMode", false);

        }

        bool isactive = true;
        private bool autostart;

        public Form1(bool minimized)
        {
            this.autostart = minimized;
            InitializeComponent();
            KeyboardHook hook = new KeyboardHook();

            // register the event that is fired after the key press.
            hook.KeyPressed +=
                new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            // register the control + alt + F12 combination as hot key.
            hook.RegisterHotKey(0, Keys.Pause);
        }


        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            // show the keys pressed in a label.
            if (isactive)
            {
                DisableMonitor("SAM0699");
                DisableMonitor("SAM0B65");
            }
            else
            {
                EnableMonitor("SAM0699", 1050, 1680, -1680, 0, 1);
                EnableMonitor("SAM0B65", 1920, 1080, 1920, 0, 0);
            }
            isactive = !isactive;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisableMonitor("SAM0699");
            DisableMonitor("SAM0B65");
            /*
            foreach (Screen screen in Screen.AllScreens)
            {
                DEVMODE dm = new DEVMODE();
                dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                EnumDisplaySettings(screen.DeviceName, ENUM_CURRENT_SETTINGS, ref dm);

                Console.WriteLine($"Real Resolution: {dm.dmFields}");

                //if (ScreenInterrogatory.DeviceFriendlyName(screen) == "BenQ GL2750H") {
                if (ScreenInterrogatory.DeviceFriendlyName(screen) == "SMB2240W") {
                    dm.dmSize = (short)Marshal.SizeOf(dm);
                    dm.dmPelsWidth = 0;
                    dm.dmPelsHeight = 0;
                    dm.dmPosition = new POINTL(0, 0);
                    dm.dmFields = (int)(DisplayModeFlags.PelsWidth | DisplayModeFlags.PelsHeight | DisplayModeFlags.Position);
                    DISP_CHANGE res = ChangeDisplaySettingsEx(screen.DeviceName, ref dm, IntPtr.Zero, ChangeDisplaySettingsFlags.CDS_RESET | ChangeDisplaySettingsFlags.CDS_UPDATEREGISTRY, IntPtr.Zero);
                    Console.WriteLine("result = " + res.ToString());
                }
            }
             */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SAM0B65
            //BNQ78AD
            //SAM0699
            EnableMonitor("SAM0699", 1050, 1680, -1680, 0, 1);
            EnableMonitor("SAM0B65", 1920, 1080, 1920, 0, 0);
        }

        private void EnableMonitor(string monitor_name, int width, int height, int x, int y, int orientation)
        {
            DISPLAY_DEVICE d = new DISPLAY_DEVICE();
            d.cb = Marshal.SizeOf(d);
            try
            {
                for (uint id = 0; EnumDisplayDevices(null, id, ref d, 0); id++)
                {
                    Console.WriteLine(
                        String.Format("{0}, {1}, {2}, {3}, {4}, {5}",
                                 id,
                                 d.DeviceName,
                                 d.DeviceString,
                                 d.StateFlags,
                                 d.DeviceID,
                                 d.DeviceKey
                                 )
                                  );


                    DISPLAY_DEVICE de = new DISPLAY_DEVICE();
                    de.cb = Marshal.SizeOf(d);
                    EnumDisplayDevices(d.DeviceName, 0, ref de, 0);

                    Console.WriteLine(
                    String.Format("{0}, {1}, {2}, {3}, {4}, {5}",
                             de.DeviceName,
                             de.DeviceString,
                             de.StateFlags,
                             de.DeviceID,
                             de.DeviceKey,
                             de.DeviceID.Split('\\')[1]
                             )
                    );

                    if (de.DeviceID.Split('\\')[1] == monitor_name)
                    {
                        DEVMODE dm = new DEVMODE();
                        dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                        EnumDisplaySettings(d.DeviceName, ENUM_CURRENT_SETTINGS, ref dm);


                        Console.WriteLine($"Device: {dm.dmNup}");
                        Console.WriteLine($"Real Resolution: {dm.dmPelsWidth}x{dm.dmPelsHeight}");

                        dm.dmPosition = new POINTL(0, 0);
                        dm.dmFields = (int)(DisplayModeFlags.Position);
                        DISP_CHANGE res = ChangeDisplaySettingsEx(d.DeviceName, ref dm, IntPtr.Zero, ChangeDisplaySettingsFlags.CDS_NORESET | ChangeDisplaySettingsFlags.CDS_UPDATEREGISTRY, IntPtr.Zero);
                        Console.WriteLine("result = " + res.ToString());
                        ChangeDisplaySettings(0, 0);


                        DEVMODE dmi = new DEVMODE();
                        dmi.dmSize = (short)Marshal.SizeOf(dmi);
                        dmi.dmPelsWidth = width;
                        dmi.dmPelsHeight = height;
                        dmi.dmDisplayFrequency = 60;
                        dmi.dmBitsPerPel = 32;
                        dmi.dmDisplayOrientation = orientation;
                        dmi.dmPosition = new POINTL(x, y);
                        dmi.dmFields = (int)(DisplayModeFlags.Position | DisplayModeFlags.PelsWidth | DisplayModeFlags.PelsHeight | DisplayModeFlags.DisplayFrequency | DisplayModeFlags.BitsPerPixel | DisplayModeFlags.DisplayOrientation);
                        DISP_CHANGE resu = ChangeDisplaySettingsEx(d.DeviceName, ref dmi, IntPtr.Zero, ChangeDisplaySettingsFlags.CDS_RESET | ChangeDisplaySettingsFlags.CDS_UPDATEREGISTRY, IntPtr.Zero);
                        Console.WriteLine("result = " + resu.ToString());
                    }


                    d.cb = Marshal.SizeOf(d);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("{0}", ex.ToString()));
            }
        }

        private void DisableMonitor(string monitor_name)
        {
            DISPLAY_DEVICE d = new DISPLAY_DEVICE();
            d.cb = Marshal.SizeOf(d);
            try
            {
                for (uint id = 0; EnumDisplayDevices(null, id, ref d, 0); id++)
                {
                    Console.WriteLine(
                        String.Format("{0}, {1}, {2}, {3}, {4}, {5}",
                                 id,
                                 d.DeviceName,
                                 d.DeviceString,
                                 d.StateFlags,
                                 d.DeviceID,
                                 d.DeviceKey
                                 )
                                  );


                    DISPLAY_DEVICE de = new DISPLAY_DEVICE();
                    de.cb = Marshal.SizeOf(d);
                    EnumDisplayDevices(d.DeviceName, 0, ref de, 0);

                    Console.WriteLine(
                    String.Format("{0}, {1}, {2}, {3}, {4}, {5}",
                             de.DeviceName,
                             de.DeviceString,
                             de.StateFlags,
                             de.DeviceID,
                             de.DeviceKey,
                             de.DeviceID.Split('\\')[1]
                             )
                    );

                    if (de.DeviceID.Split('\\')[1] == monitor_name)
                    {
                        DEVMODE dm = new DEVMODE();
                        dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                        EnumDisplaySettings(d.DeviceName, ENUM_CURRENT_SETTINGS, ref dm);


                        dm.dmSize = (short)Marshal.SizeOf(dm);
                        dm.dmPelsWidth = 0;
                        dm.dmPelsHeight = 0;
                        dm.dmPosition = new POINTL(0, 0);
                        dm.dmFields = (int)(DisplayModeFlags.PelsWidth | DisplayModeFlags.PelsHeight | DisplayModeFlags.Position);
                        DISP_CHANGE res = ChangeDisplaySettingsEx(d.DeviceName, ref dm, IntPtr.Zero, ChangeDisplaySettingsFlags.CDS_RESET | ChangeDisplaySettingsFlags.CDS_UPDATEREGISTRY, IntPtr.Zero);
                        Console.WriteLine("result = " + res.ToString());
                    }


                    d.cb = Marshal.SizeOf(d);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("{0}", ex.ToString()));
            }
        }

        private static DISPLAY_DEVICE GetDisplayDevice(int id)
        {
            var d = new DISPLAY_DEVICE();
            d.cb = Marshal.SizeOf(d);
            if (!EnumDisplayDevices(null, (uint)id, ref d, 0))
                throw new NotSupportedException("Could not find a monitor with id " + id);
            return d;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (IsAdministrator())
            {
                checkBox1.Enabled = true;
                button4.Enabled = false;
            }

            checkBox1.Checked = IsInStartup();
            checkBox1.CheckedChanged += new System.EventHandler(StartupCheckedChanged);

            DrawScreens();

            if (autostart)
            {
                Console.WriteLine("AutoStart - Minimized start");
                this.Hide();
            }
        }

        private void DrawScreens() {
            DISPLAY_DEVICE d = new DISPLAY_DEVICE();
            d.cb = Marshal.SizeOf(d);
            try
            {
                for (uint id = 0; EnumDisplayDevices(null, id, ref d, 0); id++)
                {
                    DISPLAY_DEVICE de = new DISPLAY_DEVICE();
                    de.cb = Marshal.SizeOf(d);
                    EnumDisplayDevices(d.DeviceName, 0, ref de, 0);

                    DEVMODE dm = new DEVMODE();
                    dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                    EnumDisplaySettings(d.DeviceName, ENUM_CURRENT_SETTINGS, ref dm);

                    if (de.StateFlags.HasFlag(DisplayDeviceStateFlags.AttachedToDesktop))
                    {
                        DisplayModel m = new DisplayModel(de.DeviceID.Split('\\')[1]);
                        m.Height = dm.dmPelsHeight;
                        m.Width = dm.dmPelsWidth;
                        m.X = dm.dmPosition.x;
                        m.Y = dm.dmPosition.y;
                        displayViewerPanel1.Displays[de.DeviceName] = m;
                    }
                    else {
                        if (displayViewerPanel1.Displays[de.DeviceName] is DisplayModel dispm) {
                            dispm.Disabled = true;
                        }
                    }

                    d.cb = Marshal.SizeOf(d);
                }
            }
            catch { }
            displayViewerPanel1.DrawDisplay();
        }


        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ShouldClose)
            {
                e.Cancel = true;
            }
            Hide();
        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
            startInfo.Verb = "runas";
            startInfo.Arguments = "-autostartup";
            System.Diagnostics.Process.Start(startInfo);
            ShouldClose = true;
            System.Windows.Forms.Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShouldClose = true;
            System.Windows.Forms.Application.Exit();
        }

        private void StartupCheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                SetStartup(true);
            }
            else
            {
                SetStartup(false);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawScreens();
        }
    }
}
