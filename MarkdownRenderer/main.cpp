#include <iostream>
#include <string>
#include "MarkdownRenderer.h"

int main() {
    std::string markdown = R"(
# Welcome to Markdown Renderer

This is a **bold** and *italic* text.

## Lists

1. First item
2. Second item
3. Third item

- Unordered item 1
- Unordered item 2

## Code

Inline `code` example.

```cpp
int main() {
    std::cout << "Hello, World!" << std::endl;
    return 0;
}
```

> This is a blockquote.

[Link to Google](https://www.google.com)

![Image Alt Text](https://en.wikipedia.org/wiki/Adolf_Hitler#/media/File:Hitler_portrait_crop.jpg)

---

### That's all folks!
)";

    std::cout << MarkdownRenderer::render(markdown);
    return 0;
}