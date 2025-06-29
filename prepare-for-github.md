# Hướng dẫn chuẩn bị TomDev Package cho GitHub

## Bước 1: Cập nhật thông tin trong package.json
Thay đổi các thông tin sau trong `package.json`:
- `your-email@example.com` → email thật của bạn
- `your-username` → username GitHub của bạn
- `https://github.com/your-username/TomDev-Core.git` → URL repository thật

## Bước 2: Cập nhật README.md
Thay đổi các thông tin sau trong `README.md`:
- `your-username` → username GitHub của bạn
- `your-email@example.com` → email thật của bạn

## Bước 3: Tạo GitHub Repository
1. Đăng nhập vào GitHub
2. Tạo repository mới với tên `TomDev-Core`
3. Chọn Public hoặc Private
4. KHÔNG tạo README, .gitignore, hoặc LICENSE (đã có sẵn)

## Bước 4: Push code lên GitHub
```bash
# Di chuyển vào thư mục package
cd Packages/TomDev

# Khởi tạo git repository
git init

# Thêm tất cả files
git add .

# Commit đầu tiên
git commit -m "Initial commit: TomDev Core package v1.0.0"

# Thêm remote origin
git remote add origin https://github.com/your-username/TomDev-Core.git

# Push lên GitHub
git push -u origin main
```

## Bước 5: Tạo Release trên GitHub
1. Vào repository trên GitHub
2. Click "Releases" → "Create a new release"
3. Tag version: `v1.0.0`
4. Title: `TomDev Core v1.0.0`
5. Description: Copy nội dung từ CHANGELOG.md
6. Publish release

## Bước 6: Cài đặt package trong Unity project khác
Thêm vào `Packages/manifest.json`:
```json
{
  "dependencies": {
    "com.tomdev.core": "https://github.com/your-username/TomDev-Core.git#v1.0.0"
  }
}
```

## Bước 7: Xóa code cũ từ Assets (tùy chọn)
Sau khi đã push package lên GitHub và test thành công:
```bash
# Xóa thư mục cũ
rm -rf Assets/Scripts/TomDev
```

## Lưu ý quan trọng:
- Đảm bảo tất cả .meta files được commit
- Test package trong Unity project mới trước khi release
- Cập nhật version number khi có thay đổi
- Tạo tag cho mỗi version release 