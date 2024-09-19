use std::error::Error;
use std::fmt;

#[derive(Debug)]
pub enum MorseError {
    InvalidCharacter(char),
    InvalidMorseCode(String),
}

impl fmt::Display for MorseError {
    fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
        match self {
            MorseError::InvalidCharacter(c) => write!(f, "Invalid character in input: {}", c),
            MorseError::InvalidMorseCode(s) => write!(f, "Invalid Morse code sequence: {}", s),
        }
    }
}

impl Error for MorseError {}

pub fn validate_text_input(input: &str) -> Result<(), MorseError> {
    for c in input.chars() {
        if !c.is_ascii_alphanumeric() && !c.is_ascii_punctuation() && !c.is_whitespace() {
            return Err(MorseError::InvalidCharacter(c));
        }
    }
    Ok(())
}

pub fn validate_morse_input(input: &str) -> Result<(), MorseError> {
    for sequence in input.split_whitespace() {
        if !sequence.chars().all(|c| c == '.' || c == '-') {
            return Err(MorseError::InvalidMorseCode(sequence.to_string()));
        }
    }
    Ok(())
}

pub fn handle_error<T: fmt::Display>(error: T) {
    eprintln!("Error: {}", error);
}