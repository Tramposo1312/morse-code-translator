mod translator;
mod cli;
mod utils;

use clap::Parser;
use crate::translator::MorseTranslator;
use crate::cli::{Cli, Commands};
use crate::utils::handle_error;

fn main() {
    let cli = Cli::parse();
    let translator = MorseTranslator::new();

    match &cli.command {
        Some(Commands::ToMorse { text }) => {
            match translator.to_morse(text) {
                Ok(morse) => println!("{}", morse),
                Err(e) => handle_error(e),
            }
        }
        Some(Commands::FromMorse { morse }) => {
            match translator.from_morse(morse) {
                Ok(text) => println!("{}", text),
                Err(e) => handle_error(e),
            }
        }
        None => {
            println!("Welcome to the Morse Code Translator!");
            println!("Usage:");
            println!("  To translate text to Morse code:");
            println!("    morse_code_translator to-morse <TEXT>");
            println!("  To translate Morse code to text:");
            println!("    morse_code_translator from-morse <MORSE>");
            println!("\nFor more information, use the --help option.");
        }
    }
}