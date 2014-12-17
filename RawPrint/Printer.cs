﻿using System;
using System.IO;
using System.Linq;

namespace RawPrint
{
    public class Printer
    {
        public static void PrintFile(string printer, string path, string documentName)
        {
            using (var stream = File.OpenRead(path))
            {
                PrintStream(printer, stream, documentName);
            }
        }

        public static void Printfile(string printer, string path)
        {
            PrintFile(printer, path, path);
        }

        public static void PrintStream(string printer, Stream stream, string documentName)
        {
            var defaults = new PRINTER_DEFAULTS
            {
                DesiredPrinterAccess = PRINTER_ACCESS_MASK.PRINTER_ACCESS_USE
            };

            using (var safePrinter = SafePrinter.OpenPrinter(printer, ref defaults))
            {
                DocPrinter(safePrinter, documentName, IsXPSDriver(safePrinter) ? "XPS_PASS" : "RAW", stream);
            }
        }

        private static bool IsXPSDriver(SafePrinter printer)
        {
            var files = printer.GetPrinterDriverDependentFiles();

            return files.Any(f => f.EndsWith("-pipelineconfig.xml", StringComparison.InvariantCultureIgnoreCase));
        }

        private static void DocPrinter(SafePrinter printer, string documentName, string dataType, Stream stream)
        {
            try
            {
                Console.WriteLine("Printing to type " + dataType);
                var di1 = new DOC_INFO_1
                {
                    pDataType = dataType,
                    pDocName = documentName,
                };

                printer.StartDocPrinter(di1);
                PagePrinter(printer, stream);
            }
            finally
            {
                printer.EndDocPrinter();
            }
        }

        private static void PagePrinter(SafePrinter printer, Stream stream)
        {
            try
            {
                printer.StartPagePrinter();
                WritePrinter(printer, stream);
            }
            finally
            {
                printer.EndPagePrinter();
            }
        }

        private static void WritePrinter(SafePrinter printer, Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            const int bufferSize = 1048576;
            var buffer = new byte[bufferSize];

            int read;
            while ((read = stream.Read(buffer, 0, bufferSize)) != 0)
            {
                printer.WritePrinter(buffer, read);
            }
        }
    }
}