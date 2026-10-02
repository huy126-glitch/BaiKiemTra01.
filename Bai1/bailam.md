# I. PHẦN LÝ THUYẾT & CÂU HỎI NGẮN

### Câu 1: Phân biệt Value Types và Reference Types

- **Value Types (kiểu giá trị):** Biến lưu trực tiếp giá trị của nó. Khi gán một biến cho biến khác, giá trị được sao chép sang biến mới. Ví dụ: `int`, `float`, `double`, `bool`, `struct`.
- **Reference Types (kiểu tham chiếu):** Biến lưu tham chiếu đến đối tượng được cấp phát trên Heap. Khi gán một biến cho biến khác, hai biến có thể cùng tham chiếu đến một đối tượng. Ví dụ: `class`, `string`, `array`, `object`.

Về bộ nhớ, **Value Type thường được lưu trên Stack khi là biến cục bộ**, còn đối tượng của **Reference Type thường được lưu trên Heap**. Tuy nhiên, vị trí lưu trữ còn phụ thuộc vào ngữ cảnh nên không nên hiểu tuyệt đối rằng mọi Value Type đều ở Stack và mọi Reference Type đều ở Heap.

---

### Câu 2: Init-only Properties (`init`) khác gì `set`?

- `set`: Cho phép thay đổi giá trị thuộc tính sau khi đối tượng đã được tạo.
- `init`: Chỉ cho phép gán giá trị khi khởi tạo đối tượng hoặc trong constructor. Sau khi khởi tạo xong thì không thể thay đổi giá trị bằng cách gán thông thường.

**Ví dụ:**
```csharp
class Student
{
    public string Name { get; init; }
}
```

```csharp
Student sv = new Student { Name = "Nam" };
// sv.Name = "An"; // Lỗi
```

**Trường hợp sử dụng thực tế:** `init` phù hợp với các thuộc tính cần **bất biến sau khi khởi tạo**, chẳng hạn như mã sinh viên, mã tài khoản hoặc mã đơn hàng.

---

### Câu 3: Phân biệt `virtual` và `override`

- **`virtual`** được khai báo ở **lớp cha**, cho phép phương thức được lớp con ghi đè.
- **`override`** được khai báo ở **lớp con**, dùng để cung cấp cách triển khai mới cho phương thức `virtual` của lớp cha.

Ví dụ:
```csharp
class Animal
{
    public virtual void Sound()
    {
        Console.WriteLine("Âm thanh");
    }
}

class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Gâu gâu");
    }
}
```

Khi gọi phương thức thông qua đối tượng lớp con, chương trình sẽ thực hiện phiên bản `override`. Đây là cơ chế **đa hình (Polymorphism)**.

---

### Câu 4: Tại sao `static` không truy xuất thông qua Object Instance?

Thành phần `static` **thuộc về lớp (Class)** chứ không thuộc về từng đối tượng được tạo bằng `new`.

Vì vậy, `static` được dùng trực tiếp thông qua **tên lớp**:

```csharp
class Student
{
    public static int Count = 0;
}

Student.Count++;
```

Không nên truy xuất như:

```csharp
Student sv = new Student();
sv.Count++; // Không được truy cập static theo instance
```
Lý do là tất cả các đối tượng của lớp đều dùng **chung một thành viên static**, trong khi thành viên thông thường thuộc về từng đối tượng riêng biệt.
