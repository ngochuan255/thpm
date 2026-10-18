<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:output method="html" encoding="UTF-8"
                indent="yes"/>

    <xsl:template match="/">
        <html>
            <head>
                <meta charset="UTF-8"/>
                <title>Danh sách bệnh nhân</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        margin: 40px;
                    }

                    .header {
                        display: flex;
                        justify-content: space-between;
                        margin-bottom: 15px;
                    }

                    .header div {
                        width: 48%;
                    }

                    .header-right {
                        text-align: right;
                    }

                    h3 {
                        margin: 0 0 10px 0;
                    }

                    table {
                        width: 100%;
                        border-collapse: collapse;
                        margin-bottom: 35px;
                    }

                    th, td {
                        border: 1px solid black;
                        padding: 10px;
                        text-align: center;
                    }

                    th {
                        background-color: #e6e6e6;
                    }
                </style>
            </head>

            <body>
                <xsl:for-each select="BVDK/Khoa">

                    <div class="header">
                        <div>
                            <h3>BỆNH VIỆN ĐA KHOA</h3>
                            Khoa:
                            <xsl:value-of select="TenKhoa"/>
                        </div>

                        <div class="header-right">
                            <h3>DANH SÁCH BỆNH NHÂN</h3>
                            Phòng:
                            <xsl:value-of select="Phong"/>
                        </div>
                    </div>

                    <table>
                        <tr>
                            <th>Mã số BN</th>
                            <th>Họ tên</th>
                            <th>Ngày nhập viện</th>
                            <th>Số ngày điều trị</th>
                            <th>Số tiền phải trả</th>
                        </tr>

                        <xsl:for-each select="BenhNhan">
                            <tr>
                                <td>
                                    <xsl:value-of select="@MasoBN"/>
                                </td>

                                <td>
                                    <xsl:value-of select="HoTen"/>
                                </td>

                                <td>
                                    <xsl:value-of select="NgayNhapVien"/>
                                </td>

                                <td>
                                    <xsl:value-of select="NgayDieuTri"/>
                                </td>

                                <td>
                                    <xsl:value-of
                                      select="NgayDieuTri * 15000"/>
                                </td>
                            </tr>
                        </xsl:for-each>
                    </table>

                </xsl:for-each>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
