# MyCertListNet
Console list Certificates

************************************************************

MyCertLis.exe /?

Prints to the console a list of installed personal certificates.
MyCertLis.exe [/I[:FIELDS]] [/S[:FIELDS]] [/A] [/B] [/L]

            /I    Issuer data printing.");
                  You can specify a filter for fields issuer output to the console. For example "/I:CN,OU"
            /S    Subject data printing.");
                  You can specify a filter for fields ubject output to the console. For example "/S:SN,STREET"
            /A    NotAfter date time printing.
            /B    NotBefore date time printing.
            /P    Printing a dividing line (empty line).
            /?    Printing this help.
            Running without key prints the maximum available information to the console.
            

************************************************************
Example:
------------------------------------------------------------
MyCertLis.exe /I /B 

Issuer: CN="Issuer name", O="Issuer secon name", OU=Certikicate center, STREET="221B Baker Street", E=test@test.ru
NotBefore: 15.04.2021 10:04:07
Issuer: CN=DOMEN\dmitriy
NotBefore: 11.05.2021 17:26:40

------------------------------------------------------------
MyCertLis.exe /I:CN,E /P

Issuer: CN="Issuer name", E=test@test.ru

Issuer: CN=DOMEN\dmitriy

************************************************************
PS/
It was conceived for two tasks:
1. Collecting information about all installed personal certificates in the domain network;
2. Control of expiration dates of personal electronic digital signatures by means of zabbix sender.


