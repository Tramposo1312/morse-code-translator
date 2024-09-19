use morse_code_translator::translator::MorseTranslator;

#[test]
fn test_full_translation_cycle() {
    let translator = MorseTranslator::new();
    let original_text = "HELLO WORLD";
    let morse = translator.to_morse(original_text).unwrap();
    let decoded_text = translator.from_morse(&morse).unwrap();
    assert_eq!(original_text, decoded_text)
}

#[test]
fn test_cli_to_morse() {
    let output = std::process::Command::new("cargo")
        .arg("run")
        .arg("--")
        .arg("to-morse")
        .arg("SOS")
        .output()
        .expect("Failed to execute command");
    assert!(output.status.success());
    assert_eq!(String::from_utf8_lossy(&output.stdout).trim(), "... --- ...");
}

#[test]
fn test_cli_from_morse() {
    let output = std::process::Command::new("cargo")
        .arg("run")
        .arg("--")
        .arg("from-morse")
        .arg("... --- ...")
        .output()
        .expect("Failed to execute command");

    assert!(output.status.success());
    assert_eq!(String::from_utf8_lossy(&output.stdout).trim(), "SOS");
}