NeosIT.IIS.Authentication.PkiAuthentication
===========================================
The PkiAuthentication solution consists of two projects. 

First one is the PkiAuthenticationModule, which implements IHttpModule and intercepts the IIS AuthenticateRequest to plug in your own
custom AuthenticationService using the client certificate (HttpClientCertificate). IAuthenticationService defines a simple interface
which is used in the module to determine whether an user is authenticated or not.

The second project is the so called PkiAuthenticationService (didn't find a better name for it ;) ), which implements 
IAuthenticationService and contains the actual logic to check whether user is authenticated or not. This is were your implementation
takes place ;)

The AuthenticationServiceInjector uses Ninject to inject the PkiAuthenticationService (or better an instance of an 
IAuthenticationService) into the PkiAuthenticationModule. It also takes care that the PkiAuthenticationModule is registered
on IIS startup, so you don't have to worry about registering the module in your web.config.

Features
--------

 * Supports IIS6 & IIS7 (tested with .NET 4.0 only, will do some more tests in the near future)
 * Easy handling and customizing

How to use?
-----------
Simple. All you need to do is implement your authentication algorithm (i've seen some validation against a web service recently) in
PkiAuthenticationService. Compile your project, and put the binaries into your "IIS\{Project}\bin" directory. Done ;)

References
----------
Original idea of implementing by John DeVight (http://www.aspnetwiki.com/asp-net-webforms:implementing-pki-authentication)

Contact
-------
falcon4u [AT] gmx [DOT] net / http://twitter.com/prunkstar

License
-------
Copyright (C) 2012  Florian Weinert

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
