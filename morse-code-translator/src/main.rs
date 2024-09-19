mod translator;
mod cli;

use clap::Parser;
use crate::translator::MorseTranslator;
use crate::cli::{Cli, Commands};

fn main() {
    let cli = Cli::parse();
    let translator = MorseTranslator::new();

    match &cli.command {
        Commands::ToMorse { text } => {
            println!("{}", translator.to_morse(text));
        }
        Commands::FromMorse { morse } => {
            println!("{}", translator.from_morse(morse));
        }
    }
}