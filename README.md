# TomDevCore v2.0.1

TomDevCore là Unity package cung cấp các module tiện ích cho quản lý dữ liệu (DataSO), hệ thống popup, notification, button tuỳ biến, v.v. Chuẩn hoá theo best practice, dễ mở rộng, dễ tích hợp.

## Dependencies (Bắt buộc)
- [TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest) (có sẵn trong Unity, cần import vào project)
- [DOTween](http://dotween.demigiant.com/) (import từ Asset Store hoặc website)
- [UniTask](https://github.com/Cysharp/UniTask) (import từ GitHub hoặc OpenUPM)

> **Lưu ý:** Các dependency này được tham chiếu qua assembly definition, bạn cần import đủ để tránh lỗi biên dịch.

## Cài đặt
1. Clone/copy package vào thư mục `Packages/tomdevcore` hoặc import qua git nếu đã public.
2. Đảm bảo đã import đủ các dependency trên.
3. Thêm prefab `PopupManager` vào scene, gán Canvas và các prefab notification, popup mẫu nếu muốn.

## Sử dụng nhanh
### Hiện Popup
```csharp
// Tạo prefab kế thừa TomPopup, đặt vào Resources
PopupManager.Instance.Show<MyCustomPopup>();
```

### Hiện Notification
```csharp
PopupManager.Instance.ShowNotification("Nội dung thông báo!");
```

### Button tuỳ biến
```csharp
// Gán TomButton vào bất kỳ GameObject UI nào
myTomButton.RegisterOnClick(() => {
    Debug.Log("Button clicked!");
});
```

## Các module chính
- **TomDev.DataSO**: Quản lý dữ liệu dạng ScriptableObject, hỗ trợ auto-save, PlayerPrefs, operator overload, custom data.
- **TomDev.GUI**: Hệ thống popup, notification, button tuỳ biến, hỗ trợ animation, sound, background, v.v.

## Update log
- **2.0.1**: Yêu cầu TextMeshPro, UniTask, DOTween. Notification dùng TMP. Popup hỗ trợ async UniTask. Fix minor bugs.
- **2.0.0**: Chuẩn hoá package, thêm nhiều tuỳ chọn cho popup, button, sample, hướng dẫn chi tiết.

---

Mọi thắc mắc/cần hỗ trợ, liên hệ tác giả hoặc tạo issue trên repo! 