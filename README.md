# TomDev Core

A comprehensive Unity package providing core utilities and data management systems for game development.

## 🚀 Features

### TomDev.DataSO
ScriptableObject-based data management system with automatic PlayerPrefs integration:

- **ScriptableInt**: Integer value storage with operator overloads
- **ScriptableFloat**: Float value storage with operator overloads  
- **ScriptableBool**: Boolean value storage with operator overloads
- **ScriptableString**: String value storage with helper methods
- **ScriptableCustom<T>**: Generic class for custom data types
- **Auto-save**: Automatic PlayerPrefs integration when enabled
- **Context menus**: Built-in validation and utility functions
- **Optional Odin Inspector Integration**: Enhanced inspector when Odin Inspector is available

## 📦 Installation

### Prerequisites
This package requires **Odin Inspector** to be installed in your project.

### Via Git URL
Add this to your `Packages/manifest.json`:
```json
{
  "dependencies": {
    "com.tomdev.core": "https://github.com/your-username/TomDev-Core.git"
  }
}
```

### Via OpenUPM
```bash
openupm add com.tomdev.core
```

## 🎮 Quick Start

### Creating Data Assets
1. Right-click in Project window
2. Create → TomDev → DataSO → [Data Type]
3. Configure Saved and PrefKey settings
4. Use enhanced inspector if Odin Inspector is available

### Using in Code
```csharp
using TomDev.DataSO;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScriptableInt playerScore;
    [SerializeField] private ScriptableBool isFirstTime;
    
    void Start()
    {
        // Get values
        int score = playerScore.Value;
        bool firstTime = isFirstTime.Value;
        
        // Set values
        playerScore.SetValue(1000);
        isFirstTime.SetValue(false);
        
        // Use operator overloads
        playerScore++; // Increment
        playerScore += 100; // Add 100
    }
}
```

### Enhanced Inspector Features (Optional)
If you have Odin Inspector installed, you get:
- **Buttons**: Direct access to common actions
- **Validation**: Required fields and value constraints
- **Conditional Display**: Show/hide fields based on conditions
- **Better Organization**: Titles and property ordering

## 📚 Documentation

See the [Documentation](Documentation~/README.md) folder for detailed usage examples and API reference.

## 🔧 Dependencies

- **Unity 2022.3+**: Minimum Unity version
- **Odin Inspector** (Optional): For enhanced inspector features

### Installing Odin Inspector (Optional)
If you want enhanced inspector features:
1. Purchase Odin Inspector from Unity Asset Store
2. Import it into your project
3. The package will automatically detect and enable enhanced features

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🆘 Support

- 📧 Email: your-email@example.com
- 🐛 Issues: [GitHub Issues](https://github.com/your-username/TomDev-Core/issues)
- 📖 Wiki: [GitHub Wiki](https://github.com/your-username/TomDev-Core/wiki)

## 🔮 Roadmap

- [ ] TomDev.Cheat - Cheat system and debug tools
- [ ] TomDev.GUI - UI utilities and components
- [ ] TomDev.Audio - Audio management system
- [ ] TomDev.Network - Networking utilities
- [ ] TomDev.AI - AI and pathfinding tools

---

Made with ❤️ by TomDev 