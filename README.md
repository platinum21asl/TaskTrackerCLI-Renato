# Task Tracker CLI

Task Tracker CLI adalah aplikasi berbasis command line yang digunakan untuk mengelola daftar tugas. Aplikasi ini memungkinkan Anda menambahkan, memperbarui, menghapus, menandai status, dan menampilkan tugas yang disimpan dalam file JSON.
Aplikasi ini dibuat menggunakan bahasa pemograman c#.
## Fitur

- **Menambahkan tugas baru:** Tambahkan tugas dengan deskripsi yang diberikan.
- **Memperbarui tugas:** Perbarui deskripsi tugas berdasarkan ID.
- **Menghapus tugas:** Hapus tugas berdasarkan ID.
- **Menandai tugas:** Tandai tugas sebagai `todo`, `in-progress`, atau `done`.
- **Menampilkan tugas:** Lihat semua tugas atau filter berdasarkan status.
- **Menyimpan data dalam file JSON:** Semua data tugas disimpan dalam file `tasks.json`.

## Struktur Data Tugas

Setiap tugas memiliki properti berikut:
- **id:** ID unik untuk tugas.
- **description:** Deskripsi tugas.
- **status:** Status tugas (`todo`, `in-progress`, atau `done`).
- **createdAt:** Waktu saat tugas dibuat.
- **updatedAt:** Waktu saat tugas terakhir diperbarui.

## Cara Menggunakan

### 1. Clone Repository
Clone repository ini ke komputer Anda


### 2. Jalankan Aplikasi
dotnet run [command] [options]

### 3. Perintah CLI
add [description]
update [id] [description]
delete [id]
mark-in-progress [id]
mark-done [id]
list
list [status]

## File JSON
Semua data tugas disimpan dalam file tasks.json di direktori kerja. File ini akan dibuat secara otomatis jika belum ada.
[
  {
    "id": 1,
    "description": "Belajar ASP.NET Core",
    "status": "todo",
    "createdAt": "2025-01-02T10:00:00",
    "updatedAt": "2025-01-02T10:00:00"
  }
]

## Contoh Penggunaan
### Menambahkan Tugas Baru
dotnet run add "Belajar ASP.NET Core"

### Memperbarui Tugas
dotnet run update 1 "Belajar ASP.NET Core dan CLI"

