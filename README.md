botcoin
=======

BTC Automated Trade Platform


Structural updates to make:
Improve Transaction/Trade classes to be coupled better

CurrencyWallet/Exchange should be less dependant. 
Currently the Exchange must understand what type of wallet it is dealing and how to populate it's values.
Perhaps a synthetic wallet can be built from individual ones, and the individual ones should update as the Exchange class
should be the only class that deals with data transfer.

Use Pub/Sub type architecture

Don't use Decimal data type! It's a floating point
