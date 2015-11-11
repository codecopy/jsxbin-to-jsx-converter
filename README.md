# Introduction to JSXBIN to JSX Converter
JSXBIN is a binary format of JSX, which is a superset of JavaScript made by Adobe for automating certain tasks in Adobe products such as Photoshop. Sometimes it's useful to decode and read JSXBIN files but since there's no official decoder available, here is an alternative instead.

# Usage
1. Download the [latest versîon from the Releases page](https://github.com/autoboosh/jsxbin-to-jsx-converter/releases)
2. Extract the converter
2. Run jsxbin_to_jsx on your command line using the following syntax:

```
jsxbin_to_jsx  --jsxbin <encoded-jsxbin-filepath> --jsx <decoded-jsx-filepath>
```

The converter automatically formats the code using [JsBeautifier](https://github.com/ghost6991/Jsbeautifier).

The decoder has only been tested with version 2 jsxbin files (@JSXBIN@ES@2.0@). If your file is a different version it is not guaranteed to work.

# Known Issues
The decoded code has the following issues:

* Nested if statements instead of if-else if-else
* Excessive use of parentheses
* postfix/prefix increment operations (e.g. i++) are pretty printied on the same line as the next line, causing compilation issues. The workaround is to manually put a newline after the increment expression

# Tests
The Tests-Project contains one single test. This test decodes all jsxbin-Files found in the testfiles folder comparing them with their jsx-File equivalent, also found in the same folder.

# Feedback
If you encounter any problems or have any feedback, please open an issue.