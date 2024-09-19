mod translator;
mod cli;
mod utils;
mod terminal_ui;

use clap::Parser;
use morse_code_translator::translator::MorseTranslator;
use morse_code_translator::cli::{Cli, Commands};
use morse_code_translator::utils::handle_error;
use terminal_ui::run_terminal_ui;

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
        Some(Commands::Interactive) => {
            if let Err(e) = run_terminal_ui() {
                handle_error(e);
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