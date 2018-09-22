Perform String Functions in the Windows Console.
> 1. Download [exe file](https://raw.githubusercontent.com/winp/extra-bel/master/ecd.cmd).
> 2. Copy to `C:\Program_Files\Scripts`.
> 3. Add `C:\Program_Files\Scripts` to `PATH` environment variable.


```batch
> estring [--input|-i <input string>] [--regex|-r] [--encoded|e <encoding format>] <function> <args>...

:: [] -> optional argument
:: <> -> argument value
```


### size

```batch
:: get size of a string
> estring [options] size
```

```batch
:: get size of input string specified as argument
> estring --input "Sudo Permission" size

:: get size of input string from stdin (extra \r\n included in echo)
> echo Sudo Permission|estring size

:: get size of input string from file
> cat file.txt|estring size
```


### get

```batch
:: get a part of input string
> estring [options] get [<begin>] [<length>]
```

```batch
:: get first character in input string
> estring -i "Application Developer" get

:: get 7 index character in input string
> estring --escaped code -i "Euro\r\n2016" get 7

:: get 4 characters from 3 index character in input string
> cat keys.txt|estring get 3 4
```


### range

```batch
:: get specified range of input string
> estring [options] range [<begin>] [<end>]
```

```batch
:: get part of input string starting from 5 index character
> estring -i "Munnabhai MBBS" 5

:: get part of input file starting from 8 index character to 35
> cat handsetpool|estring range 8 35
```


### find

```batch
:: find index of string in the input string
> estring [options] find [<search>] [<begin>] [<direction>]
```

```batch
:: find first index of "in" in input string
> estring -i "initializing os/core" find "in"

:: find first index of "in" in input string begining from index 3
> estring -i "initializing os/drivers" find "in" 3

:: find 3 matches of "in" in input string from index 2
> estring -i "initializing os/kernel" find "in" 2 3

:: find 5 matches of "elf" in input file from index 343 in reverse direction
> cat lotr.txt|estring find "elf" 343 -5

:: find all matches of regular expression "Hal(o|ley)" in input file
> cat masters_of_the_universe.txt|estring -r find "Hal(o|ley)"
```


### compare

```batch
:: compare two strings
> estring [options] compare <compared string>
```

```batch
:: compare input string with "Birds"
> estring -i "Angry" compare "Birds"

:: compare input string with regular expression "W.+g"
> estring -r -i "Warthog" compare "W.+g"
```


### startswith

```batch
:: check whether input string starts with prefix
> estring [options] startswith [<prefix>]
```

```batch
:: check whether input string starts with "meteor"
> estring -i "meteorite" startswith "meteor"

:: check whether input file starts with regular expression "join/i"
> cat deadsec.grp|estring -r startswith "join/i"
```


### endswith

```batch
:: check whether input string ends with suffix
> estring [options] endswith [<suffix>]
```

```batch
:: check whether input string ends with "crack"
> estring -i "floor crack" endswith "crack"

:: check whether input file ends with regular expression "haleem\r\n/i"
> cat menu.txt|estring -e c -r endswith "haleem\r\n/i"
```


### code

```batch
:: get character code for each character in input string
> estring [options] code
```

```batch
:: get character code for all characters in input string
> estring -i "Ring" code

:: get character code for 6 index character in input file
> cat cats.txt|estring get 6|estring code
```


### encode

```batch
:: encode or escape string to coded form
> estring [options] encode [<type>]
```

```batch
:: encode input string to html text type
> estring -i "i love chocos > cornflakes" encode html

:: encode input string to url text type
> estring -i "http://www.thiswillneverexist.com" encode url

:: encode input string to dos batch text type
> estring -i "@hyd after 99% in insti | 1st in cntr-strk" encode dos

:: encode input string to dos batch extensions enabled mode text type
> estring -i "mcd, i am hating it!! :(" encode dose

:: encode input string to regex text type
> estring -i "cracking 2-45.gsm through bounced link pcrs.citycom.com" encode regex

:: encode input file to code text type
> cat target_199.exploit|estring encode c > target_199.exploit.code
```


### decode

```batch
:: decode or unescape a string to original form
> estring [options] decode [<type>]
```

```batch
:: decode html text type input string to original
> estring -i "i love chocos &gt; cornflakes" decode h

:: decode url text type input string to original form
> estring -i "http%3a%2f%2fwww.thiswillneverexist.com" decode u

:: decode dos batch text type input string to original form
> estring -i "@hyd after 99%% in insti ^| 1st in cntr-strk" decode d

:: decode dos batch extensions enabled mode text type input string to original form
> estring -i "mcd, i am hating it^^!^^! :(" decode e

:: decode regex text type input string to original form
> estring -i "cracking\ 2-45\.gsm\ through\ bounced\ link\ pcrs\.citycom\.com" decode r

:: decode code text type input file to original file
> cat target_199.exploit.code|estring decode code > target_199.exploit
```


### line

```batch
:: replace line endings in input string
> estring [options] line [<ending>]
```

```batch
:: replace unix line ending in input string to windows
> estring -e code -i "Red Pill\nBlue Pill\n" line "\r\n"

:: replace windows line ending in input file to unix
> cat profile.info|estring -e c line "\n"

:: replace line ending in input file to ";"
> cat watershed.log|estring line ";"
```


### copy

```batch
:: copy input string specified number of times
> estring [options] copy [<times>]
```

```batch
:: copy input string 5 times
> estring -i "i-win." 5
```


### format

```batch
:: use input string as format to embed parameter strings
> estring [options] format [<parameter>]...
```

```batch
:: yet another way to read "Alice in the Wonderland"
> estring -i "{0} in the {1}" format "Alice" "Wonderland"
```


### pad

```batch
:: pad input string on the left and/or right
> estring [options] [<times>] [<direction>] [<pad string>]
```

```batch
:: pad input string with 1 space on left and right
> estring -i "Pillar of Autumn" pad

:: pad input string with 4 spaces on left and right
> estring -i "Halo" pad 4

:: pad input string with 8 spaces on the left only
> estring -i "Truth and Reconciliation" 8 -1

:: pad input string with 56 "." on the right only
> estring -i "The Silent Cartographer" 56 1 "."
```


### trim

```batch
:: trim input string on the left and/or right
> estring [options] trim [<direction>] [<trim characters>]
```

```batch
:: trim input string on the left and right, of white space
> estring -i "  Assault on the Control Room  " trim

:: trim input string on the right only, of white space
> estring -e c -i " 343 Guilty Spark\t\t\t" trim 1

:: trim input string on left and right of whitespace and "."
> estring -e c -i " ...The Library... " trim 0 " \t\r\n."
```


### add

```batch
:: add a string to input string
> estring [options] add [<add string>] [<index>]
```

```batch
:: add a string to the end of input string
> estring -i "Two " add "Betrayals"

:: add a string at index 1 of input string
> estring -i "Kes" add "ey" 1
```


### put

```batch
:: put a string onto input string at specified index
> estring [options] put [<put string>] [<index>]
```

```batch
:: put a string at the begining of input string
> estring -i "One Maw" put "The"

:: put a string at index 7 of input string
> estring -i "Battle Fight" put "Creek" 7
```


### replace

```batch
:: replace a search string with new string in input string
> estring [options] replace [<search string>] [<new string>]
```

```batch
:: remove all "oo" from input string
> estring -i "Blood Gulch" replace "oo"

:: replace all "o" with "a" in input string
> estring -i "Boarding Action" replace "o" "a"

:: replace regular expression "(ll)|(ut)" with "n" in input string
> estring -r -i "Chill Out" replace "(ll)|(ut)" "n"
```


### remove

```batch
:: remove part of input string
> estring [options] remove [<size>] [<index>]
```

```batch
:: remove " loaded" of size 7 from input string
> estring -i "Chiron TL-43 loaded" remove 7

:: remove "Your " of size 5 at index 0 from input string
> estring -i "Your Damnation" remove 5 0
```


### reverse

```batch
:: reverse a string
> estring [options] reverse
```

```batch
:: reverse the input string
> estring -i "noynaC regnaD" reverse
```


### lowercase

```batch
:: convert input string to lower case
> estring [options] lowercase
```

```batch
:: lower case the input string
> estring -i "Death Island" lowercase
```


### uppercase

```batch
:: convert input string to upper case
> estring [options] uppercase
```

```batch
:: upper case the input string
> estring -i "Derelict" uppercase
```


