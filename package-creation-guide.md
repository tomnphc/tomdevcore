# Hướng dẫn tạo Unity Package chi tiết

## 📋 Các bước tạo Unity Package

### 1. **Cấu trúc thư mục chuẩn**
```
Packages/YourPackage/
├── Runtime/           # Code chạy trong game
│   ├── Scripts/       # Scripts chính
│   └── YourPackage.asmdef
├── Editor/            # Code chỉ chạy trong editor
│   ├── Scripts/       # Editor scripts
│   └── YourPackage.Editor.asmdef
├── Documentation~/    # Tài liệu (không build)
├── Samples~/          # Ví dụ (không build)
├── Tests/             # Unit tests
├── package.json       # Manifest file
├── README.md          # Mô tả package
├── CHANGELOG.md       # Lịch sử version
└── LICENSE            # Giấy phép
```

### 2. **Tạo Assembly Definition Files**

#### Runtime Assembly (YourPackage.asmdef):
```json
{
    "name": "YourPackage",
    "rootNamespace": "YourNamespace",
    "references": [
        "Sirenix.OdinInspector.Attributes",
        "Sirenix.OdinInspector.Editor"
    ],
    "includePlatforms": [],
    "excludePlatforms": [],
    "allowUnsafeCode": false,
    "overrideReferences": false,
    "precompiledReferences": [],
    "autoReferenced": true,
    "defineConstraints": [],
    "versionDefines": [],
    "noEngineReferences": false
}
```

#### Editor Assembly (YourPackage.Editor.asmdef):
```json
{
    "name": "YourPackage.Editor",
    "rootNamespace": "YourNamespace",
    "references": [
        "YourPackage"
    ],
    "includePlatforms": [
        "Editor"
    ],
    "excludePlatforms": [],
    "allowUnsafeCode": false,
    "overrideReferences": false,
    "precompiledReferences": [],
    "autoReferenced": true,
    "defineConstraints": [],
    "versionDefines": [],
    "noEngineReferences": false
}
```

### 3. **Package Manifest (package.json)**
```json
{
  "name": "com.yourcompany.yourpackage",
  "version": "1.0.0",
  "displayName": "Your Package Name",
  "description": "Description of your package",
  "unity": "2022.3",
  "dependencies": {
    "com.sirenix.odininspector": "3.2.1",
    "com.unity.textmeshpro": "3.0.6"
  },
  "keywords": [
    "your",
    "package",
    "keywords"
  ],
  "author": {
    "name": "Your Name",
    "email": "your-email@example.com",
    "url": "https://github.com/your-username"
  },
  "license": "MIT",
  "repository": {
    "type": "git",
    "url": "https://github.com/your-username/your-package.git"
  },
  "bugs": {
    "url": "https://github.com/your-username/your-package/issues"
  },
  "homepage": "https://github.com/your-username/your-package#readme",
  "samples": [
    {
      "displayName": "Basic Example",
      "description": "Basic usage example",
      "path": "Samples~/BasicExample"
    }
  ]
}
```

## 🔧 Thêm Dependencies

### **Các loại dependencies:**

#### 1. **Unity Packages (Built-in)**
```json
"dependencies": {
  "com.unity.textmeshpro": "3.0.6",
  "com.unity.inputsystem": "1.4.4"
}
```

#### 2. **Third-party Packages (Odin Inspector)**
```json
"dependencies": {
  "com.sirenix.odininspector": "3.2.1"
}
```

#### 3. **Git Dependencies**
```json
"dependencies": {
  "com.other.package": "https://github.com/username/repo.git#v1.0.0"
}
```

#### 4. **Local Dependencies**
```json
"dependencies": {
  "com.local.package": "file:../LocalPackage"
}
```

### **Cách tìm Package ID và Version:**

#### Unity Registry Packages:
1. Mở Package Manager
2. Chọn package
3. Copy Package ID từ URL hoặc manifest

#### Odin Inspector:
- Package ID: `com.sirenix.odininspector`
- Version: `3.2.1` (hoặc version bạn đang dùng)

## 🎯 Sử dụng Odin Inspector trong Package

### **1. Thêm using statement:**
```csharp
using Sirenix.OdinInspector;
```

### **2. Sử dụng attributes:**
```csharp
[Title("Settings")]
[SerializeField, ToggleLeft] private bool enabled = true;

[ShowIf("enabled")]
[SerializeField, MinValue(0), MaxValue(100)] private int value = 50;

[Button("Reset")]
public void ResetValue()
{
    value = 0;
}
```

### **3. Các attributes hữu ích:**
- `[Title]` - Tiêu đề section
- `[Button]` - Tạo button trong inspector
- `[ShowIf]` - Hiển thị field khi điều kiện đúng
- `[HideIf]` - Ẩn field khi điều kiện đúng
- `[ReadOnly]` - Field chỉ đọc
- `[MinValue]`, `[MaxValue]` - Giới hạn giá trị
- `[Required]` - Field bắt buộc
- `[PropertyOrder]` - Sắp xếp thứ tự properties

## 📦 Publishing Package

### **1. Local Testing:**
```json
// Trong manifest.json của project test
{
  "dependencies": {
    "com.yourpackage": "file:../YourPackage"
  }
}
```

### **2. Git Repository:**
```json
{
  "dependencies": {
    "com.yourpackage": "https://github.com/username/repo.git#v1.0.0"
  }
}
```

### **3. OpenUPM (Nếu public):**
```bash
openupm add com.yourpackage
```

## ⚠️ Lưu ý quan trọng:

1. **Version Compatibility**: Đảm bảo dependencies tương thích
2. **Assembly References**: Thêm đúng references trong .asmdef
3. **Meta Files**: Commit tất cả .meta files
4. **Documentation**: Viết README và CHANGELOG rõ ràng
5. **Testing**: Test package trong project mới trước khi release
6. **License**: Chọn license phù hợp (MIT, Apache, etc.)

## 🚀 Best Practices:

1. **Namespace**: Sử dụng namespace riêng cho package
2. **Error Handling**: Xử lý lỗi khi dependencies không có
3. **Backward Compatibility**: Đảm bảo tương thích ngược
4. **Performance**: Tối ưu performance cho runtime
5. **Documentation**: Viết documentation chi tiết
6. **Examples**: Cung cấp ví dụ sử dụng 