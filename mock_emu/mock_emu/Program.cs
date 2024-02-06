using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        var currentMemoryMB = 100;
        const int targetMemoryMB = 300; // Target memory in MB
        const int sleepIntervalMS = 500; // Sleep interval between allocations in milliseconds
        IntPtr pointer = Marshal.AllocHGlobal(1024);

        while (currentMemoryMB < targetMemoryMB)
        {
            var sz = currentMemoryMB++ * 1024 * 1024;
            pointer = Marshal.AllocHGlobal(sz);
            Console.WriteLine($"Allocated {currentMemoryMB}MB of memory.");

            // Simulate work by writing to the allocated memory
            unsafe
            {
                byte* ptr = (byte*)pointer.ToPointer();
                for (long j = 0; j < sz; j++)
                {
                    ptr[j] = 1;
                }
            }


            Thread.Sleep(sleepIntervalMS); // Sleep for a short interval to control the rate of allocation
            Marshal.FreeHGlobal(pointer);
        }

        Console.WriteLine("Test Complete");
    }
}
