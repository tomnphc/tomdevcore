# Hướng dẫn đẩy TomDev Package lên GitHub

## ⚠️ Vấn đề với Odin Inspector

**KHÔNG BAO GIỜ** đẩy Odin Inspector lên GitHub vì:
- ❌ **License Restrictions**: Asset thương mại
- ❌ **Copyright**: Thuộc về Sirenix
- ❌ **File Size**: Rất lớn (hàng trăm MB)
- ❌ **Legal Issues**: Vi phạm bản quyền

## ✅ Giải pháp: Optional Dependency

Package đã được cấu hình để:
- ✅ Hoạt động **không cần** Odin Inspector
- ✅ Tự động **detect** Odin Inspector nếu có
- ✅ **Enhanced features** khi có Odin Inspector
- ✅ **Standard Unity inspector** khi không có

## 🚀 Các bước đẩy lên GitHub

### 1. **Cập nhật thông tin cá nhân**

#### Trong `package.json`:
```json
{
  "author": {
    "name": "TomDev",
    "email": "your-real-email@example.com",
    "url": "https://github.com/your-real-username"
  },
  "repository": {
    "type": "git",
    "url": "https://github.com/your-real-username/TomDev-Core.git"
  },
  "bugs": {
    "url": "https://github.com/your-real-username/TomDev-Core/issues"
  },
  "homepage": "https://github.com/your-real-username/TomDev-Core#readme"
}
```

#### Trong `README.md`:
- Thay `your-username` → username GitHub thật
- Thay `your-email@example.com` → email thật

### 2. **Tạo GitHub Repository**

1. Đăng nhập GitHub
2. Click "New repository"
3. Repository name: `TomDev-Core`
4. Description: `Core package for TomDev framework`
5. Chọn **Public** hoặc **Private**
6. **KHÔNG** tạo README, .gitignore, LICENSE (đã có sẵn)
7. Click "Create repository"

### 3. **Push code lên GitHub**

```bash
# Di chuyển vào thư mục package
cd Packages/TomDev

# Khởi tạo git repository
git init

# Thêm tất cả files (KHÔNG bao gồm Odin Inspector)
git add .

# Commit đầu tiên
git commit -m "Initial commit: TomDev Core package v1.0.0

- ScriptableObject data management system
- Optional Odin Inspector integration
- PlayerPrefs auto-save functionality
- Operator overloads for easy usage
- Comprehensive documentation"

# Thêm remote origin
git remote add origin https://github.com/your-real-username/TomDev-Core.git

# Push lên GitHub
git push -u origin main
```

### 4. **Tạo Release**

1. Vào repository trên GitHub
2. Click "Releases" → "Create a new release"
3. Tag version: `v1.0.0`
4. Title: `TomDev Core v1.0.0`
5. Description: Copy nội dung từ `CHANGELOG.md`
6. Publish release

### 5. **Test Package**

Tạo project Unity mới để test:

```json
// Packages/manifest.json
{
  "dependencies": {
    "com.tomdev.core": "https://github.com/your-real-username/TomDev-Core.git#v1.0.0"
  }
}
```

## 📋 Checklist trước khi push

- [ ] ✅ Cập nhật thông tin cá nhân trong `package.json`
- [ ] ✅ Cập nhật thông tin cá nhân trong `README.md`
- [ ] ✅ Xóa tất cả references đến Odin Inspector trong dependencies
- [ ] ✅ Sử dụng conditional compilation (`#if ODIN_INSPECTOR`)
- [ ] ✅ Test package hoạt động không có Odin Inspector
- [ ] ✅ Test package hoạt động có Odin Inspector
- [ ] ✅ Commit tất cả .meta files
- [ ] ✅ Tạo GitHub repository
- [ ] ✅ Push code lên GitHub
- [ ] ✅ Tạo release với tag

## 🔧 Cách sử dụng package

### **Không có Odin Inspector:**
- Package hoạt động bình thường
- Sử dụng standard Unity inspector
- Tất cả tính năng core vẫn hoạt động

### **Có Odin Inspector:**
- Package tự động detect
- Enhanced inspector với buttons
- Validation và conditional display
- Better organization

## 🎯 Lợi ích của approach này

1. **Legal Compliance**: Không vi phạm bản quyền
2. **Flexibility**: Hoạt động với và không có Odin Inspector
3. **User Choice**: Người dùng tự quyết định
4. **Small Size**: Package nhỏ gọn
5. **Easy Distribution**: Dễ chia sẻ và cài đặt

## 📞 Hỗ trợ

Nếu gặp vấn đề:
1. Kiểm tra console logs
2. Sử dụng menu "TomDev/Check Odin Inspector"
3. Tạo issue trên GitHub
4. Liên hệ qua email 