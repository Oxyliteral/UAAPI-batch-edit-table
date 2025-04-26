Uses UAssetAPI:

https://github.com/atenfyr/UAssetAPI

Place uasset and uexp file in the same directory, then edit the bat file accordingly.

Mainly created for Triangle Strategy. Should work for other games too (?).

ConsoleApp1.exe "uassetfilepath" "tablefiltername" "propertyfiltername" "value to replace"


Works with double, float, int, nameproperty, and boolean.


Value to replace can be a set of calculations for double and float. Separate by spaces. No limit to amount.

+num adds to value.
-num subtracts to value;
*num multiples value;
/num divides value;

r rounds to nearest.
rd rounds down.
ru rounds up.

So, for example, "*1.2 r" would multiply value by 1.2, then round.

"+1 /2 -3 rd" would add 1, divide by 2, subtract 3, then round down.

For bool, can be True or False.

For int, can be +, -, *, and /, but the number must be an int.
