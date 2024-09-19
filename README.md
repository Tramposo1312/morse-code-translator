# Morse Code Translator

A command-line tool written in Rust that translates text to Morse code and vice versa.

## Features

- Translate text to Morse code and vice versa.
- Support for letters (A-Z), numbers (0-9), and common punctuation marks

## Usage

To translate text to Morse code:
```
cargo run -- to-morse "Hello, World!"
```

To translate Morse code to text:
```
cargo run -- from-morse ".... . .-.. .-.. --- --..--  .-- --- .-. .-.. -.. -.-.--"
```

## Installation

Clone the repository and build the project:

```
git clone https://github.com/Tramposo1312/morse-code-translator.git
cd morse-code-translator
cargo build --release
```

The executable will be available in `target/release/morse_code_translator`.
