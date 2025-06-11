Model與ViewModel
Model存放普通類別
ViewModel存放顯示網站用類別
Utilty存放自定義函式
Controller處理網站邏輯
static的使用可以將類別變成靜態並直接呼叫類別函式

 u => u.Username == model.Username &&
            u.PasswordHash == hash
lambda表達式
意旨找到某變數u滿足=>後面的條件式

套件使用
Microsoft.EntityFrameworkCore.Sqlite
Microsoft.EntityFrameworkCore.Tools

資料庫相關指令
dotnet ef migrations add InitialCreate
dotnet ef database update