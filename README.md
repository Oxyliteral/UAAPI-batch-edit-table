Uses UAssetAPI:

https://github.com/atenfyr/UAssetAPI

Place uasset and uexp file in the same directory, then edit the bat file accordingly.

Mainly created for Triangle Strategy.

ConsoleApp1.exe "uassetfilepath" "tablefiltername" "propertyfiltername" "value to replace"


Works with double, float, and boolean.


Value to replace can be a set of calculations for double and float. Separate by spaces.

+num adds to value.
-num subtracts to value;
*num multiples value;
/num divides value;

r rounds to nearest.
rd rounds down.
ru rounds up.

So, for example, "*1.2 r" would multiply value by 1.2, then round.
