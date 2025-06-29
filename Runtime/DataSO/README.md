# TomDev.DataSO Package

Package này cung cấp các ScriptableObject để lưu trữ và quản lý dữ liệu trong Unity với khả năng tự động lưu vào PlayerPrefs.

## Các Class Chính

### ScriptableDataBase
Base class cho tất cả các scriptable object data với các tính năng:
- `Saved`: Boolean để bật/tắt lưu vào PlayerPrefs
- `PrefKey`: Key để lưu trong PlayerPrefs
- `CheckExistedKey()`: Context menu để kiểm tra key đã tồn tại chưa

### ScriptableInt
Lưu trữ giá trị int:
```csharp
// Tạo asset
ScriptableInt playerLevel = ScriptableObject.CreateInstance<ScriptableInt>();

// Sử dụng
int level = playerLevel.Value;
playerLevel.SetValue(10);

// Operator overloads
playerLevel++; // Tăng giá trị
playerLevel += 5; // Cộng thêm 5
```

### ScriptableFloat
Lưu trữ giá trị float:
```csharp
ScriptableFloat playerHealth = ScriptableObject.CreateInstance<ScriptableFloat>();

float health = playerHealth.Value;
playerHealth.SetValue(100f);

// Operator overloads
playerHealth *= 1.5f; // Nhân với 1.5
```

### ScriptableBool
Lưu trữ giá trị bool:
```csharp
ScriptableBool isGamePaused = ScriptableObject.CreateInstance<ScriptableBool>();

bool paused = isGamePaused.Value;
isGamePaused.SetValue(true);

// Operator overloads
!isGamePaused; // Toggle giá trị
```

### ScriptableString
Lưu trữ giá trị string:
```csharp
ScriptableString playerName = ScriptableObject.CreateInstance<ScriptableString>();

string name = playerName.Value;
playerName.SetValue("Player1");

// Helper methods
if (playerName.IsEmpty) { /* ... */ }
playerName.Clear();
```

### ScriptableCustom<T>
Lưu trữ các kiểu dữ liệu custom (sử dụng JSON serialization):
```csharp
// Định nghĩa custom class
[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int level;
    public float experience;
}

// Sử dụng
ScriptableCustom<PlayerData> playerData = ScriptableObject.CreateInstance<ScriptableCustom<PlayerData>>();

PlayerData data = playerData.Value;
data.level = 10;
playerData.SetValue(data);
```

## Cách Sử Dụng

### 1. Tạo Asset
- Chuột phải trong Project window
- Create > TomDev > DataSO > [Loại dữ liệu mong muốn]

### 2. Cấu Hình
- Tick vào "Saved" nếu muốn lưu vào PlayerPrefs
- Nhập "PrefKey" để làm key lưu trữ
- Sử dụng context menu "Check Existed Key" để kiểm tra

### 3. Sử Dụng Trong Code
```csharp
public class GameManager : MonoBehaviour
{
    [SerializeField] private ScriptableInt playerScore;
    [SerializeField] private ScriptableBool isFirstTime;
    [SerializeField] private ScriptablePlayerData playerData;
    
    void Start()
    {
        // Lấy giá trị
        int score = playerScore.Value;
        bool firstTime = isFirstTime.Value;
        
        // Set giá trị
        playerScore.SetValue(1000);
        isFirstTime.SetValue(false);
        
        // Với custom data
        var data = playerData.Value;
        data.level = 5;
        playerData.SetValue(data);
    }
}
```

## Ví Dụ Cụ Thể

Package bao gồm các ví dụ:
- `PlayerData`: Dữ liệu người chơi
- `GameSettings`: Cài đặt game
- `ScriptablePlayerData`: ScriptableObject cho PlayerData
- `ScriptableGameSettings`: ScriptableObject cho GameSettings

## Lưu Ý

1. Khi `Saved = true`, dữ liệu sẽ tự động lưu vào PlayerPrefs
2. `PrefKey` sẽ tự động được set bằng tên asset nếu để trống
3. Tất cả các class đều có operator overloads để sử dụng dễ dàng
4. Custom data sử dụng JSON serialization, đảm bảo class có `[System.Serializable]`
5. Context menu "Check Existed Key" chỉ hoạt động khi `Saved = true` 