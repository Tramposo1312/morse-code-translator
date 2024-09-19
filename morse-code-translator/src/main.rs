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
        Commands::ToMorse { text } => {
            match translator.to_morse(text) {
                Ok(morse) => println!("{}", morse),
                Err(e) => handle_error(e),
            }
        }
        Commands::FromMorse { morse } => {
            match translator.from_morse(morse) {
                Ok(text) => println!("{}", text),
                Err(e) => handle_error(e),
            }
        }
    }
}