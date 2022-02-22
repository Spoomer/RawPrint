RawPrint
========

.Net Standard library to send files directly to a Windows printer bypassing the printer driver.

Send PostScript, PCL or other print file types directly to a printer.


Usage:

        using RawPrint;
	
        // Create an instance of the Printer
        IPrinter printer = new Printer();
    
        // Print the file
        printer.PrintRawFile(PrinterName, Filepath, Filename);

	
Forked from ekonbenefits, who forked from frogmorecs 
till Version 0.5.0 orignally from: 
Copyright (c) Frogmore Computer Services Ltd

*2019-09-12 Version 0.5.0*

Job arguments now include the printer name.
License moved to Frogmore Computer Services Ltd (still BSD).

*2018-01-24 Version 0.4.0*

IPrinter now includes a *OnJobCreated* event which fires just as the job is started, use this to modify the job information.

*2017-02-02 Version 0.3.0*

If you supply a page count this is now reflected in the print job information in the spooled document.

*2016-02-21 Version 0.2.0*

Static methods are now obsolete.
Introduced IPrinter interface to make mocking easier.
Support for spooling a paused print job

*2015-10-20 Version 0.1.0*

Fixed an issue with some HP drivers that misname their pipelineconfig.xml file.
