# Các ràng buộc XSD cơ bản và ví dụ code

Các ví dụ dùng **XSD 1.0**, với `xmlns:xs="http://www.w3.org/2001/XMLSchema"` khai báo trên `xs:schema`. Mỗi mục gồm yêu cầu, code XSD và dữ liệu đúng/sai.

Các đoạn `simpleType` đặt trực tiếp trong `xs:schema`; khai báo phần tử con đặt trong mô hình nội dung của `complexType`, thuộc tính đặt sau mô hình nội dung. Mục 15 có cặp XSD/XML hoàn chỉnh để chạy thử.

## 1. `xs:pattern` — Quy định định dạng

**Yêu cầu:** Mã sinh viên có đúng 8 chữ số, giữ được số 0 ở đầu.

```xml
<xs:simpleType name="MaSVType">
  <xs:restriction base="xs:string">
    <xs:pattern value="[0-9]{8}"/>
  </xs:restriction>
</xs:simpleType>

<xs:element name="MaSV" type="MaSVType"/>
```

```xml
<!-- Hợp lệ -->
<MaSV>09121111</MaSV>

<!-- Không hợp lệ: có chữ cái -->
<MaSV>0912111A</MaSV>
```

| Mẫu | Ý nghĩa | Giá trị hợp lệ |
| --- | --- | --- |
| `[0-9]{8}` | Đúng 8 chữ số | `09121111` |
| `[A-Z]{3}` | Đúng 3 chữ in hoa | `ABC` |
| `[a-z]{2,5}` | Từ 2 đến 5 chữ thường | `abc` |
| `[A-Za-z0-9]+` | Một hoặc nhiều chữ cái Latin không dấu/chữ số | `SV123` |
| `0[0-9]{9}` | Đúng 10 chữ số, bắt đầu bằng 0 | `0912345678` |
| `BN[0-9]{2}` | BN theo sau bởi 2 chữ số | `BN01` |

Regex XSD 1.0 khớp toàn bộ chuỗi. Không thêm `^` và `$` như regex JavaScript vì chúng không đóng vai trò neo đầu/cuối trong cú pháp này.

## 2. `xs:length` — Độ dài chính xác

**Yêu cầu:** Mã loại hàng dài đúng 3 ký tự.

```xml
<xs:simpleType name="MaLoaiType">
  <xs:restriction base="xs:string">
    <xs:length value="3"/>
  </xs:restriction>
</xs:simpleType>

<xs:element name="MaLoai" type="MaLoaiType"/>
```

- Hợp lệ: `<MaLoai>ABC</MaLoai>`.
- Không hợp lệ: `<MaLoai>AB</MaLoai>`, `<MaLoai>ABCD</MaLoai>`.

`length` chỉ xét độ dài; `123` cũng hợp lệ. Muốn giới hạn loại ký tự, thêm `pattern`.

## 3. `xs:minLength` — Độ dài tối thiểu

**Yêu cầu:** Họ tên có ít nhất 3 ký tự sau khi chuẩn hóa khoảng trắng.

```xml
<xs:simpleType name="HoTenType">
  <xs:restriction base="xs:token">
    <xs:minLength value="3"/>
  </xs:restriction>
</xs:simpleType>

<xs:element name="HoTen" type="HoTenType"/>
```

- Hợp lệ: `<HoTen>Nguyễn An</HoTen>`.
- Không hợp lệ: `<HoTen>An</HoTen>`, `<HoTen>   </HoTen>`.

`xs:token` bỏ khoảng trắng XML đầu/cuối và gộp khoảng trắng liên tiếp. Nếu dùng `xs:string`, ba dấu cách vẫn có thể thỏa `minLength="3"`.

## 4. `xs:maxLength` — Độ dài tối đa

**Yêu cầu:** Mã hàng dài từ 1 đến 6 ký tự.

```xml
<xs:simpleType name="MaHangType">
  <xs:restriction base="xs:string">
    <xs:minLength value="1"/>
    <xs:maxLength value="6"/>
  </xs:restriction>
</xs:simpleType>

<xs:element name="MaHang" type="MaHangType"/>
```

- Hợp lệ: `<MaHang>H12345</MaHang>`.
- Không hợp lệ: `<MaHang>H123456</MaHang>`, `<MaHang/>`.

Các facet khác nhau phải cùng thỏa mãn: mã không được rỗng và không dài quá 6 ký tự.

## 5. `xs:minInclusive`, `xs:maxInclusive` — Khoảng có lấy biên

**Yêu cầu:** Điểm từ 0 đến 10, nhận cả 0 và 10.

```xml
<xs:simpleType name="DiemType">
  <xs:restriction base="xs:decimal">
    <xs:minInclusive value="0"/>
    <xs:maxInclusive value="10"/>
  </xs:restriction>
</xs:simpleType>

<xs:element name="Diem" type="DiemType"/>
```

- Hợp lệ: `<Diem>0</Diem>`, `<Diem>8.5</Diem>`, `<Diem>10</Diem>`.
- Không hợp lệ: `<Diem>-1</Diem>`, `<Diem>10.1</Diem>`.

Dùng `xs:decimal` nếu cho phép điểm lẻ; dùng `xs:integer` nếu chỉ nhận số nguyên.

## 6. `xs:minExclusive`, `xs:maxExclusive` — Khoảng không lấy biên

**Yêu cầu:** Đơn giá lớn hơn 0 và nhỏ hơn 1.000.000.

```xml
<xs:simpleType name="DonGiaType">
  <xs:restriction base="xs:decimal">
    <xs:minExclusive value="0"/>
    <xs:maxExclusive value="1000000"/>
  </xs:restriction>
</xs:simpleType>

<xs:element name="DonGia" type="DonGiaType"/>
```

- Hợp lệ: `<DonGia>0.5</DonGia>`, `<DonGia>999999</DonGia>`.
- Không hợp lệ: `<DonGia>0</DonGia>`, `<DonGia>1000000</DonGia>`.

| Ràng buộc | Điều kiện |
| --- | --- |
| `<xs:minInclusive value="0"/>` | Giá trị ≥ 0 |
| `<xs:maxInclusive value="10"/>` | Giá trị ≤ 10 |
| `<xs:minExclusive value="0"/>` | Giá trị > 0 |
| `<xs:maxExclusive value="10"/>` | Giá trị < 10 |

Nếu chỉ cần **số nguyên dương**, có thể khai báo trực tiếp:

```xml
<xs:element name="SoLuong" type="xs:positiveInteger"/>
```

`SoLuong` nhận `1`, `60`; không nhận `0`, `-1`, `1.5`. Muốn nhận cả số nguyên 0, dùng `xs:nonNegativeInteger`.

## 7. `xs:enumeration` — Chỉ nhận giá trị trong danh sách

**Yêu cầu:** Đơn vị tính chỉ là `gói`, `hộp` hoặc `chiếc`.

```xml
<xs:simpleType name="DonViTinhType">
  <xs:restriction base="xs:string">
    <xs:enumeration value="gói"/>
    <xs:enumeration value="hộp"/>
    <xs:enumeration value="chiếc"/>
  </xs:restriction>
</xs:simpleType>

<xs:element name="DonViTinh" type="DonViTinhType"/>
```

- Hợp lệ: `<DonViTinh>hộp</DonViTinh>`.
- Không hợp lệ: `<DonViTinh>Hộp</DonViTinh>`, `<DonViTinh>kg</DonViTinh>`.

Với kiểu `xs:string`, chữ hoa/thường, dấu tiếng Việt và khoảng trắng đều ảnh hưởng kết quả so sánh.

## 8. `xs:totalDigits`, `xs:fractionDigits` — Giới hạn chữ số

**Yêu cầu:** Giá trị có tối đa 6 chữ số, trong đó tối đa 2 chữ số thập phân.

```xml
<xs:simpleType name="SoTienType">
  <xs:restriction base="xs:decimal">
    <xs:totalDigits value="6"/>
    <xs:fractionDigits value="2"/>
  </xs:restriction>
</xs:simpleType>

<xs:element name="SoTien" type="SoTienType"/>
```

| Giá trị | Kết quả | Lý do |
| --- | --- | --- |
| `1234.56` | Hợp lệ | 6 chữ số, 2 chữ số thập phân |
| `123456` | Hợp lệ | 6 chữ số, không có phần thập phân |
| `12345.67` | Không hợp lệ | Có 7 chữ số |
| `123.456` | Không hợp lệ | Có 3 chữ số thập phân |
| `1.2300` | Hợp lệ | Giá trị tương đương `1.23` |

Dấu âm và dấu chấm không tính là chữ số. `fractionDigits="2"` giới hạn giá trị số, không bắt buộc viết đúng hai chữ số sau dấu chấm. Nếu cần số tiền không âm hoặc có mức tối đa cụ thể, thêm facet cận tương ứng.

## 9. `xs:whiteSpace` — Chuẩn hóa khoảng trắng

| Giá trị facet | Cách xử lý |
| --- | --- |
| `preserve` | Giữ nguyên ở bước xử lý facet |
| `replace` | Đổi tab và ký tự xuống dòng XML thành dấu cách |
| `collapse` | Thực hiện replace, gộp dấu cách liên tiếp, bỏ dấu cách đầu/cuối |

**Yêu cầu:** Tên chuẩn hóa khoảng trắng và không được rỗng.

```xml
<xs:simpleType name="TenChuanHoaType">
  <xs:restriction base="xs:token">
    <xs:whiteSpace value="collapse"/>
    <xs:minLength value="1"/>
  </xs:restriction>
</xs:simpleType>

<xs:element name="Ten" type="TenChuanHoaType"/>
```

- `<Ten>  Nguyễn   Văn An  </Ten>` hợp lệ, được xét như `Nguyễn Văn An`.
- `<Ten>   </Ten>` không hợp lệ vì sau chuẩn hóa là chuỗi rỗng.

`xs:token` vốn đã có hành vi collapse; facet được viết tường minh để minh họa cú pháp. Việc chuẩn hóa khi xác thực không có nghĩa tệp XML gốc tự được ghi lại.

## 10. `minOccurs`, `maxOccurs` — Số lần xuất hiện

**Yêu cầu:** Mỗi sinh viên có từ 3 đến 5 môn học.

```xml
<xs:element name="SinhVien">
  <xs:complexType>
    <xs:sequence>
      <xs:element name="MonHoc" type="xs:string"
                  minOccurs="3" maxOccurs="5"/>
    </xs:sequence>
  </xs:complexType>
</xs:element>
```

```xml
<!-- Hợp lệ: có 3 môn -->
<SinhVien>
  <MonHoc>XML</MonHoc>
  <MonHoc>Cơ sở dữ liệu</MonHoc>
  <MonHoc>Tích hợp phần mềm</MonHoc>
</SinhVien>
```

Có 2 hoặc 6 thẻ `MonHoc` là không hợp lệ.

| Yêu cầu | Khai báo trên phần tử con |
| --- | --- |
| Đúng 1 lần | Không ghi, hoặc `minOccurs="1" maxOccurs="1"` |
| Không có hoặc có 1 lần | `minOccurs="0" maxOccurs="1"` |
| Từ 0 đến nhiều lần | `minOccurs="0" maxOccurs="unbounded"` |
| Ít nhất 1 lần | `minOccurs="1" maxOccurs="unbounded"` |
| Từ 1 đến 4 hóa đơn | `minOccurs="1" maxOccurs="4"` |
| Đúng 2 bệnh nhân | `minOccurs="2" maxOccurs="2"` |
| Từ 1 đến 10 loại hàng | `minOccurs="1" maxOccurs="10"` |

Cả hai mặc định bằng 1. Không viết riêng `minOccurs="3"` vì `maxOccurs` vẫn bằng 1 và làm schema sai. Số thẻ dùng occurrence; số ký tự dùng length.

## 11. `use` — Thuộc tính bắt buộc, tùy chọn hoặc bị cấm

**Yêu cầu:** Hàng phải có mã; ghi chú có thể bỏ qua.

```xml
<xs:element name="Hang">
  <xs:complexType>
    <xs:attribute name="MaHang" type="xs:string" use="required"/>
    <xs:attribute name="GhiChu" type="xs:string" use="optional"/>
    <xs:attribute name="MaCu" type="xs:string" use="prohibited"/>
  </xs:complexType>
</xs:element>
```

```xml
<!-- Hợp lệ -->
<Hang MaHang="H01"/>
<Hang MaHang="H02" GhiChu="Hàng mới"/>

<!-- Không hợp lệ: thiếu MaHang -->
<Hang/>

<!-- Không hợp lệ: có thuộc tính bị cấm -->
<Hang MaHang="H03" MaCu="OLD01"/>
```

`optional` là mặc định. `required` chỉ bắt buộc thuộc tính có mặt: `MaHang=""` vẫn được nhận bởi `xs:string` nếu chưa bổ sung minLength/pattern.

## 12. `xs:sequence` — Đúng thứ tự phần tử

**Yêu cầu:** Mã sinh viên đứng trước họ tên, sau đó là ngày sinh.

```xml
<xs:element name="SinhVien">
  <xs:complexType>
    <xs:sequence>
      <xs:element name="MaSV" type="xs:string"/>
      <xs:element name="HoTen" type="xs:string"/>
      <xs:element name="NgaySinh" type="xs:date"/>
    </xs:sequence>
  </xs:complexType>
</xs:element>
```

```xml
<!-- Hợp lệ -->
<SinhVien>
  <MaSV>09121111</MaSV>
  <HoTen>Nguyễn An</HoTen>
  <NgaySinh>2005-03-05</NgaySinh>
</SinhVien>
```

Đưa `HoTen` lên trước `MaSV` là không hợp lệ. `xs:date` nhận ngày dạng `2005-03-05`, không nhận `05/03/2005`.

## 13. `xs:choice` — Chọn một nhánh

**Yêu cầu:** Liên hệ bằng email hoặc số điện thoại.

```xml
<xs:element name="LienHe">
  <xs:complexType>
    <xs:choice>
      <xs:element name="Email" type="xs:string"/>
      <xs:element name="SoDienThoai" type="xs:string"/>
    </xs:choice>
  </xs:complexType>
</xs:element>
```

```xml
<!-- Hợp lệ -->
<LienHe>
  <Email>an@example.org</Email>
</LienHe>

<!-- Không hợp lệ: chọn cả hai nhánh -->
<LienHe>
  <Email>an@example.org</Email>
  <SoDienThoai>0912345678</SoDienThoai>
</LienHe>
```

Mặc định phải chọn đúng một nhánh. Nếu cho `choice` lặp nhiều lần bằng `maxOccurs`, mỗi lần lặp được chọn một nhánh khác nhau.

## 14. `xs:all` — Không yêu cầu thứ tự

**Yêu cầu:** Có họ tên và địa chỉ, không quan trọng trường nào đứng trước.

```xml
<xs:element name="Nguoi">
  <xs:complexType>
    <xs:all>
      <xs:element name="HoTen" type="xs:string"/>
      <xs:element name="DiaChi" type="xs:string"/>
    </xs:all>
  </xs:complexType>
</xs:element>
```

```xml
<!-- Hợp lệ dù DiaChi đứng trước HoTen -->
<Nguoi>
  <DiaChi>Hà Nội</DiaChi>
  <HoTen>Nguyễn An</HoTen>
</Nguoi>
```

Thiếu `HoTen`, thiếu `DiaChi` hoặc lặp một trường là không hợp lệ với khai báo này. Trong XSD 1.0, mỗi phần tử con của `all` chỉ xuất hiện tối đa một lần; bản thân nhóm `all` cũng không được lặp nhiều lần.

## 15. `xs:unique`, `xs:key`, `xs:keyref` — Duy nhất và tham chiếu

| Ràng buộc | Ý nghĩa |
| --- | --- |
| `unique` | Những bộ giá trị đầy đủ không được trùng; trường có thể được phép thiếu |
| `key` | Mỗi bản ghi được chọn phải có bộ khóa đầy đủ, không nil và không trùng |
| `keyref` | Bộ giá trị tham chiếu đầy đủ phải khớp khóa/unique được chỉ định |

**Yêu cầu:** Mã sinh viên là khóa; email nếu có không được trùng; mã sinh viên trong đăng ký phải tồn tại.

### XSD hoàn chỉnh — `DangKy.xsd`

```xml
<?xml version="1.0" encoding="UTF-8"?>
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema">
  <xs:element name="DS">
    <xs:complexType>
      <xs:sequence>
        <xs:element name="SinhVien" maxOccurs="unbounded">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="MaSV" type="xs:string"/>
              <xs:element name="Email" type="xs:token" minOccurs="0"/>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element name="DangKy" minOccurs="0" maxOccurs="unbounded">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="MaSV" type="xs:string"/>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>

    <xs:unique name="EmailDuyNhat">
      <xs:selector xpath="SinhVien"/>
      <xs:field xpath="Email"/>
    </xs:unique>

    <xs:key name="KhoaMaSV">
      <xs:selector xpath="SinhVien"/>
      <xs:field xpath="MaSV"/>
    </xs:key>

    <xs:keyref name="ThamChieuMaSV" refer="KhoaMaSV">
      <xs:selector xpath="DangKy"/>
      <xs:field xpath="MaSV"/>
    </xs:keyref>
  </xs:element>
</xs:schema>
```

### XML hợp lệ — `DangKy.xml`

```xml
<?xml version="1.0" encoding="UTF-8"?>
<DS xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    xsi:noNamespaceSchemaLocation="DangKy.xsd">
  <SinhVien>
    <MaSV>SV01</MaSV>
    <Email>an@example.org</Email>
  </SinhVien>
  <SinhVien>
    <MaSV>SV02</MaSV>
  </SinhVien>
  <DangKy>
    <MaSV>SV02</MaSV>
  </DangKy>
</DS>
```

| Thay đổi XML trên | Kết quả |
| --- | --- |
| Đổi mã sinh viên thứ hai thành `SV01` | Sai: trùng key |
| Thêm email `an@example.org` cho sinh viên thứ hai | Sai: trùng unique |
| Đổi mã trong `DangKy` thành `SV99` | Sai: keyref không tìm thấy khóa |
| Bỏ email ở cả hai sinh viên | Đúng: trường email tùy chọn không tham gia unique khi thiếu |

Lưu ý khi viết code:

- `selector` chọn bản ghi; `field` chọn trường trong từng bản ghi.
- `xpath="MaSV"` chọn phần tử con; nếu mã là thuộc tính thì dùng `xpath="@MaSV"`.
- Đặt `unique`, `key`, `keyref` trong khai báo `element`, sau `complexType`.
- Ràng buộc đặt trên `DS` có phạm vi trong từng `DS`; đặt trên từng hóa đơn thì phạm vi là từng hóa đơn.
- Khóa đầy đủ không đồng nghĩa chuỗi khóa không rỗng; thêm minLength/pattern nếu cần.
- `keyref` không tự bắt buộc trường tham chiếu xuất hiện. Trong ví dụ này, `MaSV` của `DangKy` bắt buộc vì `minOccurs` mặc định bằng 1.

**Khóa ghép:** Nếu cần cặp mã sinh viên–mã môn duy nhất, dùng nhiều `field`. Ví dụ sau dành cho các bản ghi `DangKy` có đủ hai thuộc tính `MaSV`, `MaMon`:

```xml
<xs:unique name="DangKyKhongTrung">
  <xs:selector xpath="DangKy"/>
  <xs:field xpath="@MaSV"/>
  <xs:field xpath="@MaMon"/>
</xs:unique>
```

Đây là cấu trúc minh họa khác với XML phía trên: cặp `(SV01, XML)` không được lặp, nhưng `(SV01, XML)` và `(SV01, JAVA)` được phép.

## 16. `default`, `fixed` — Giá trị mặc định và cố định

**Yêu cầu:** Trạng thái mặc định là `Mới`; đơn vị tiền nếu khai báo phải là `VND`.

```xml
<xs:element name="HoaDon">
  <xs:complexType>
    <xs:attribute name="TrangThai" type="xs:string" default="Mới"/>
    <xs:attribute name="DonViTien" type="xs:string" fixed="VND"/>
  </xs:complexType>
</xs:element>
```

```xml
<!-- Hợp lệ: thuộc tính tùy chọn, áp dụng giá trị khi xử lý schema -->
<HoaDon/>

<!-- Hợp lệ: được thay giá trị default -->
<HoaDon TrangThai="Đã thanh toán" DonViTien="VND"/>

<!-- Không hợp lệ: khác giá trị fixed -->
<HoaDon DonViTien="USD"/>
```

`fixed` không làm thuộc tính bắt buộc xuất hiện. Muốn bắt buộc, thêm `use="required"`. Không khai báo đồng thời `default` và `fixed` trên cùng một khai báo; thuộc tính có default phải là tùy chọn.

Với **phần tử**, default áp dụng khi thẻ xuất hiện nhưng rỗng, không tự tạo thẻ bị thiếu:

```xml
<xs:element name="TrangThai" type="xs:string"
            minOccurs="0" default="Mới"/>
```

`<TrangThai/>` nhận giá trị mặc định khi xử lý schema; bỏ hẳn thẻ không có nghĩa schema tự chèn thêm thẻ.

## 17. `nillable` — Cho phép giá trị nil

**Yêu cầu:** Có thể không có thông tin học bổng; nếu có số tiền thì phải là decimal.

```xml
<xs:element name="SinhVien">
  <xs:complexType>
    <xs:sequence>
      <xs:element name="HocBong" type="xs:decimal"
                  minOccurs="0" nillable="true"/>
    </xs:sequence>
  </xs:complexType>
</xs:element>
```

```xml
<!-- Hợp lệ: có thẻ nhưng giá trị là nil -->
<SinhVien xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <HocBong xsi:nil="true"/>
</SinhVien>
```

| Nội dung trong SinhVien | Kết quả |
| --- | --- |
| Không có `HocBong` | Đúng vì `minOccurs="0"` |
| `<HocBong>0</HocBong>` | Đúng, giá trị là số 0 |
| `<HocBong xsi:nil="true"/>` | Đúng nếu đã khai báo prefix xsi |
| `<HocBong/>` | Sai: rỗng không phải decimal |
| `<HocBong xsi:nil="true">100</HocBong>` | Sai: phần tử nil không được chứa nội dung |

`nillable` mặc định là false. Cho phép nil và cho phép thiếu thẻ là hai ràng buộc riêng: dùng `nillable="true"` cho nil, `minOccurs="0"` cho thiếu thẻ.
