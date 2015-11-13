# Introduction to JSXBIN to JSX Converter
JSXBIN is a binary format of JSX, which is a superset of JavaScript made by Adobe for automating certain tasks in Adobe products such as Photoshop. Sometimes it's useful to decode and read JSXBIN files but since there's no official decoder available, here is an alternative instead.

# Usage
1. Download the [latest versîon from the Releases page](https://github.com/autoboosh/jsxbin-to-jsx-converter/releases)
2. Extract the converter
2. Run jsxbin_to_jsx on your command line using the following syntax:

```
jsxbin_to_jsx JSXBIN JSX
```

Example:

```
jsxbin_to_jsx encoded.jsxbin decoded.jsx
```

The converter automatically formats the code using [JsBeautifier](https://github.com/ghost6991/Jsbeautifier).

The decoder has only been tested with version 2 jsxbin files (@JSXBIN@ES@2.0@). If your file is a different version it is not guaranteed to work.

# Tests
The Tests-Project contains one single test. This test decodes all jsxbin-Files found in the testfiles folder comparing them with their jsx-File equivalent, also found in the same folder.

# Feedback
If you encounter any problems or have any feedback, please open an issue.