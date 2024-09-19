use std::collections::HashMap;

pub struct MorseTranslator {
    to_morse: HashMap<char, String>,
    from_morse: HashMap<String, char>,
}

impl MorseTranslator {
    pub fn new() -> Self {
        let mut to_morse = HashMap::new();
        let mut from_morse = HashMap::new();

        // Letters
        to_morse.insert('A', ".-".to_string());
        to_morse.insert('B', "-...".to_string());
        to_morse.insert('C', "-.-.".to_string());
        to_morse.insert('D', "-..".to_string());
        to_morse.insert('E', ".".to_string());
        to_morse.insert('F', "..-.".to_string());
        to_morse.insert('G', "--.".to_string());
        to_morse.insert('H', "....".to_string());
        to_morse.insert('I', "..".to_string());
        to_morse.insert('J', ".---".to_string());
        to_morse.insert('K', "-.-".to_string());
        to_morse.insert('L', ".-..".to_string());
        to_morse.insert('M', "--".to_string());
        to_morse.insert('N', "-.".to_string());
        to_morse.insert('O', "---".to_string());
        to_morse.insert('P', ".--.".to_string());
        to_morse.insert('Q', "--.-".to_string());
        to_morse.insert('R', ".-.".to_string());
        to_morse.insert('S', "...".to_string());
        to_morse.insert('T', "-".to_string());
        to_morse.insert('U', "..-".to_string());
        to_morse.insert('V', "...-".to_string());
        to_morse.insert('W', ".--".to_string());
        to_morse.insert('X', "-..-".to_string());
        to_morse.insert('Y', "-.--".to_string());
        to_morse.insert('Z', "--..".to_string());

        // Numbers
        to_morse.insert('0', "-----".to_string());
        to_morse.insert('1', ".----".to_string());
        to_morse.insert('2', "..---".to_string());
        to_morse.insert('3', "...--".to_string());
        to_morse.insert('4', "....-".to_string());
        to_morse.insert('5', ".....".to_string());
        to_morse.insert('6', "-....".to_string());
        to_morse.insert('7', "--...".to_string());
        to_morse.insert('8', "---..".to_string());
        to_morse.insert('9', "----.".to_string());

        // Punctuation
        to_morse.insert('.', ".-.-.-".to_string());
        to_morse.insert(',', "--..--".to_string());
        to_morse.insert('?', "..--..".to_string());
        to_morse.insert('\'', ".----.".to_string());
        to_morse.insert('!', "-.-.--".to_string());
        to_morse.insert('/', "-..-.".to_string());
        to_morse.insert('(', "-.--.".to_string());
        to_morse.insert(')', "-.--.-".to_string());
        to_morse.insert('&', ".-...".to_string());
        to_morse.insert(':', "---...".to_string());
        to_morse.insert(';', "-.-.-.".to_string());
        to_morse.insert('=', "-...-".to_string());
        to_morse.insert('+', ".-.-.".to_string());
        to_morse.insert('-', "-....-".to_string());
        to_morse.insert('_', "..--.-".to_string());
        to_morse.insert('"', ".-..-.".to_string());
        to_morse.insert('$', "...-..-".to_string());
        to_morse.insert('@', ".--.-.".to_string());

        // Create reverse mappings
        for (k, v) in &to_morse {
            from_morse.insert(v.clone(), *k);
        }

        MorseTranslator { to_morse, from_morse }
    }

    pub fn to_morse(&self, text: &str) -> String {
        text.to_uppercase()
            .chars()
            .map(|c| {
                if c.is_whitespace() {
                    "  ".to_string()
                } else {
                    self.to_morse.get(&c).cloned().unwrap_or_else(|| c.to_string())
                }
            })
            .collect::<Vec<String>>()
            .join(" ")
    }

    pub fn from_morse(&self, morse: &str) -> String {
        morse.split("  ")
            .map(|word| {
                word.split_whitespace()
                    .filter_map(|code| self.from_morse.get(code).cloned())
                    .collect::<String>()
            })
            .collect::<Vec<String>>()
            .join(" ")
    }
}
