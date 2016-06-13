# ostring

Perform String Functions in the Windows Console.


## usage

```batch
> ostring [--input|-i <input string>] [--regex|-r] [--encoded|e <encoding format>] <function> <args>...

:: [] -> optional argument
:: <> -> argument value
```


### size

```batch
:: get size of a string
> ostring [options] size
```

```batch
:: get size of input string specified as argument
> ostring --input "Sudo Permission" size

:: get size of input string from stdin (extra \r\n included in echo)
> echo Sudo Permission|ostring size

:: get size of input string from file
> cat file.txt|ostring size
```


### get

```batch
:: get a part of input string
> ostring [options] get [<begin>] [<length>]
```

```batch
:: get first character in input string
> ostring -i "Application Developer" get

:: get 7 index character in input string
> ostring --escaped code -i "Euro\r\n2016" get 7

:: get 4 characters from 3 index character in input string
> cat keys.txt|ostring get 3 4
```


### range

```batch
:: get specified range of input string
> ostring [options] range [<begin>] [<end>]
```

```batch
:: get part of input string starting from 5 index character
> ostring -i "Munnabhai MBBS" 5

:: get part of input file starting from 8 index character to 35
> cat handsetpool|ostring range 8 35
```


### find

```batch
:: find index of string in the input string
> ostring [options] find [<search>] [<begin>] [<direction>]
```

```batch
:: find first index of "in" in input string
> ostring -i "initializing os/core" find "in"

:: find first index of "in" in input string begining from index 3
> ostring -i "initializing os/drivers" find "in" 3

:: find 3 matches of "in" in input string from index 2
> ostring -i "initializing os/kernel" find "in" 2 3

:: find 5 matches of "elf" in input file from index 343 in reverse direction
> cat lotr.txt|ostring find "elf" 343 -5

:: find all matches of regular expression "Hal(o|ley)" in input file
> cat masters_of_the_universe.txt|ostring -r find "Hal(o|ley)"
```


### compare

```batch
:: compare two strings
> ostring [options] compare <compared string>
```

```batch
:: compare input string with "Birds"
> ostring -i "Angry" compare "Birds"

:: compare input string with regular expression "W.+g"
> ostring -r -i "Warthog" compare "W.+g"
```


### startswith

```batch
:: check whether input string starts with prefix
> ostring [options] startswith [<prefix>]
```

```batch
:: check whether input string starts with "meteor"
> ostring -i "meteorite" startswith "meteor"

:: check whether input file starts with regular expression "join/i"
> cat deadsec.grp|ostring -r startswith "join/i"
```


### endswith

```batch
:: check whether input string ends with suffix
> ostring [options] endswith [<suffix>]
```

```batch
:: check whether input string ends with "crack"
> ostring -i "floor crack" endswith "crack"

:: check whether input file ends with regular expression "haleem\r\n/i"
> cat menu.txt|ostring -e c -r endswith "haleem\r\n/i"
```


### code

```batch
:: get character code for each character in input string
> ostring [options] code
```

```batch
:: get character code for all characters in input string
> ostring -i "Ring" code

:: get character code for 6 index character in input file
> cat cats.txt|ostring get 6|ostring code
```


### encode

```batch
:: encode or escape string to coded form
> ostring [options] encode [<type>]
```

```batch
:: encode input string to html text type
> ostring -i "i love chocos > cornflakes" encode html

:: encode input string to url text type
> ostring -i "http://www.thiswillneverexist.com" encode url

:: encode input string to dos batch text type
> ostring -i "@hyd after 99% in insti | 1st in cntr-strk" encode dos

:: encode input string to dos batch extensions enabled mode text type
> ostring -i "mcd, i am hating it!! :(" encode dose

:: encode input string to regex text type
> ostring -i "cracking 2-45.gsm through bounced link pcrs.citycom.com" encode regex

:: encode input file to code text type
> cat target_199.exploit|ostring encode c > target_199.exploit.code
```


### decode

```batch
:: decode or unescape a string to original form
> ostring [options] decode [<type>]
```

```batch
:: decode html text type input string to original
> ostring -i "i love chocos &gt; cornflakes" decode h

:: decode url text type input string to original form
> ostring -i "http%3a%2f%2fwww.thiswillneverexist.com" decode u

:: decode dos batch text type input string to original form
> ostring -i "@hyd after 99%% in insti ^| 1st in cntr-strk" decode d

:: decode dos batch extensions enabled mode text type input string to original form
> ostring -i "mcd, i am hating it^^!^^! :(" decode e

:: decode regex text type input string to original form
> ostring -i "cracking\ 2-45\.gsm\ through\ bounced\ link\ pcrs\.citycom\.com" decode r

:: decode code text type input file to original file
> cat target_199.exploit.code|ostring decode code > target_199.exploit
```


### line

```batch
:: replace line endings in input string
> ostring [options] line [<ending>]
```

```batch
:: replace unix line ending in input string to windows
> ostring -e code -i "Red Pill\nBlue Pill\n" line "\r\n"

:: replace windows line ending in input file to unix
> cat profile.info|ostring -e c line "\n"

:: replace line ending in input file to ";"
> cat watershed.log|ostring line ";"
```
