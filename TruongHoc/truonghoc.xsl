<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html" encoding="UTF-8" indent="yes"/>

    <xsl:template match="/">
        <html>
            <head>
                <meta charset="UTF-8"/>
                <title>Thông tin trường học</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        margin: 30px;
                    }
                    h1 {
                        text-align: center;
                        margin-bottom: 30px;
                    }
                    .khoa-block {
                        margin-bottom: 35px;
                    }
                    .ten-khoa {
                        font-size: 22px;
                        font-weight: bold;
                        margin: 10px 0 12px 0;
                    }
                    table {
                        width: 100%;
                        border-collapse: collapse;
                    }
                    th, td {
                        border: 1px solid #000;
                        padding: 8px;
                        text-align: center;
                    }
                    th {
                        background-color: #e6e6e6;
                    }
                </style>
            </head>
            <body>
                <h1>THÔNG TIN TRƯỜNG HỌC</h1>

                <xsl:for-each select="truonghoc/khoa">
                    <div class="khoa-block">
                        <div class="ten-khoa">Khoa: <xsl:value-of select="@tenkhoa"/></div>
                        <table>
                            <tr>
                                <th>Mã sv</th>
                                <th>Họ tên</th>
                                <th>Điện thoại</th>
                                <th>Điểm</th>
                                <th>Xếp loại</th>
                            </tr>
                            <xsl:for-each select="sinhvien">
                                <tr>
                                    <td><xsl:value-of select="@masv"/></td>
                                    <td><xsl:value-of select="hoten"/></td>
                                    <td><xsl:value-of select="dienthoai"/></td>
                                    <td><xsl:value-of select="diem"/></td>
                                    <td>
                                        <xsl:choose>
                                            <xsl:when test="diem &lt; 5">Yếu</xsl:when>
                                            <xsl:when test="diem &gt;= 5 and diem &lt; 7">Trung bình</xsl:when>
                                            <xsl:when test="diem &gt;= 7 and diem &lt; 8.5">Khá</xsl:when>
                                            <xsl:otherwise>Giỏi</xsl:otherwise>
                                        </xsl:choose>
                                    </td>
                                </tr>
                            </xsl:for-each>
                        </table>
                    </div>
                </xsl:for-each>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
