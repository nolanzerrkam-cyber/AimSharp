using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AimTrainer
{
    internal class ConsoleMouse
    {
        // DLLImport is a way for me to import AND to use methode of windows.

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle); 
        
        // GetStdHandle is the method to get the handle which is a pointer that windows attribute to a system ressource to manipulate it.
        // IntPtr is a type C# used to represent a pointer
        // The handle is the value on that variable.
        // the input buffer is the space where the console recevices the mouse interaction (and everything else)

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint dwMode);


        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        /* Those two methods are going to read and modify the config of the input buffer of the console.
         * GetConsoleMode is going to read the parameters of the console and to stock them to a variable.
           SetConsoleMode is going to put a new layer of parameter to the console by using the hanfle and 
           the new mode.*/

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]

        private static extern bool ReadConsoleInput(IntPtr hConsoleInput, [Out] INPUT_RECORD[] lpBuffer, uint nLength, out uint lpNumberOfEventsRead);

        [DllImport("kernel32.dll", SetLastError = true)]

        private static extern bool GetNumberOfConsoleInputEvents(IntPtr hConsoleInput, out uint lpcNumberOfEvents);

        // basically ReadConsoleInput is a method to read the events in the input buffer of the console.

        private const int STD_INPUT_HANDLE = -10; // -10 because its a const for STD_INPUT_HANDLE to target the console input buffer. 
        private const uint ENABLE_MOUSE_INPUT = 0x0010; // 16 is the value; and its the flag to authorize the mouse input. Its a const added in the SetConsoleMode.
        private const uint ENABLE_EXTENDED_FLAGS = 0x0080; // Needed to activate ENABLE_QUICK_EDIT_MODE
        private const uint ENABLE_QUICK_EDIT_MODE = 0x0040; // For the quickedit mode. It was the problem that was selecting text and not accepting my click. This is disabling the selection of text.

        [StructLayout(LayoutKind.Explicit)]
        private struct INPUT_RECORD
        {
            [FieldOffset(0)] public ushort EventType;
            [FieldOffset(4)] public MOUSE_EVENT_RECORD MouseEvent;
        }

        private struct MOUSE_EVENT_RECORD
        {
            public COORD dwMousePosition; // position of the cursor (x,y)
            public uint dwButtonState; // 1 means the left click is done (FROM_LEFT_1ST_BUTTON_PRESSED)
                                        // 2 means the right click (RIGHTMOST_BUTTON_CLICK)
            public uint dwControlKeyState; // Shows if the control keys of the keyboard are used at the same time has the clicK; i dont use it but i need it otherwise windows dosent let me use this
            public uint dwEventFlags; // 0 ; simple click, 1:(0x0001) for a mouse mouvement; 2 for a double click and 4 for a mouse wheel interaction.
        }

        private struct COORD
        {
            public short X;
            public short Y;
        }

        private static IntPtr _consoleHandle;

        public static void EnableMouse()
        {
            _consoleHandle = GetStdHandle(STD_INPUT_HANDLE); // I need the handle to continue
            GetConsoleMode(_consoleHandle, out uint mode); // I get the consolemode, and a variable called mode will get it.

            mode &= ~ENABLE_QUICK_EDIT_MODE; // i need to disable the quickedit mode
            mode |= ENABLE_MOUSE_INPUT | ENABLE_EXTENDED_FLAGS; // then I have to activate the mouse

            SetConsoleMode(_consoleHandle, mode); // Now i set the new mode with the handle that I got.
        }

        //public static(int X, int Y) GetClickPosition()
        //{
        //    INPUT_RECORD[] record = new INPUT_RECORD[1]; // This will be important for later.
        //    // INPUT_RECORD is a type to read inputs that the user is doing, whatever it is a click of the mouse or a keyboard press.

        //    while(true) // Here, the program is still looping until a valid click is done.
        //    {
        //        ReadConsoleInput(_consoleHandle, record, 1, out uint _);
        //        // The method is waiting that the user is doing the needed action like move the mouse, click or 
        //        // pressing a button on the keyboard

        //        // Here, directly when an action is done, windows is going to put the record input into the
        //        // array of type INPUT_RECORD.

        //        if (record[0].EventType == 0x0002) // if the record is of type 0x0002, which means something coming from the mouse
        //        {
        //            var mouse = record[0].MouseEvent; // We stock the value

        //            if(mouse.dwButtonState == 1 && mouse.dwEventFlags == 0) // If the LEFT click has been done
        //                                                                    // AND it's a simple click, no double click or something else
        //            {
        //                return (mouse.dwMousePosition.X, mouse.dwMousePosition.Y); // Return the coord of the click.
        //            }
        //        }
        //    }
        //}

        public static (int X, int Y)? TryGetClickPosition()
        {
            GetNumberOfConsoleInputEvents(_consoleHandle, out uint nbEvents);

            while(nbEvents > 0)
            {
                INPUT_RECORD[] record = new INPUT_RECORD[1];
                ReadConsoleInput(_consoleHandle, record, 1, out uint _);
                nbEvents--;

                if (record[0].EventType == 0x0002)
                {
                    var mouse = record[0].MouseEvent;
                    if(mouse.dwButtonState == 1 && (mouse.dwEventFlags == 0 || mouse.dwEventFlags == 2))
                    {
                        return (mouse.dwMousePosition.X, mouse.dwMousePosition.Y);
                    }
                }
            }

            return null;
        }

        public override string ToString()
        {
            return base.ToString()!;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();                      
        }

        public override bool Equals(object? obj)
        {
            return obj.ToString() == this.ToString();
        }
    }
}
