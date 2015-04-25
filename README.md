# InverterEmulator
C# app that emulates the RS232 serial protocol for a Voltronic Axpert MKS or a PIP HS solar inverter.

Useful for developing gadgets that communicate with the inverter without having to have the actual inverter nearby, and allows you to generate the different fault codes as well.

Still pretty rough around the edges, not all messages are implemented yet. Pull requests welcome.

Protocol is partially documented in the included pdf, but it's out of date I think. I sniffed the comms and have included a dump as well, but sanitised out my serial number. 
The inverter's watchpower app works using two usb-serial adapters connected together.

I've tried to separate the UI from the comms classes, so should be easily reusable as a client. Just make more message classes.

Let me know what you manage to build... theloadshed.blogspot.com
Released under the MEH license.
