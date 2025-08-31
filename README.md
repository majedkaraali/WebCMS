# 🏥Web Clinic Management System

This project, titled **Web Clinic Management System**, is a centralized, web-based platform that streamlines clinic operations through digital means.  

It is designed for **patients, doctors, lab workers, and administrative staff**, each with customized interfaces and role-specific functionalities.  

The solution is built using modern technologies including **ASP.NET Core MVC, C#, SQL Server, and Entity Framework**.  
It follows a **modular and scalable architecture**, making it suitable for deployment in real-world clinical environments.



## 📸 Screenshots / Demo




![Screenshot](https://github.com/majedkaraali/WebCMS/blob/98a949d67030eaf7655aaf8f5f898d9c47009f01/WebCMS/wwwroot/screenshots/emr.PNG)

![Screenshot](https://github.com/majedkaraali/WebCMS/blob/2c8e3cdd4ee2357b0b3b67763c1f582f5000b6f9/WebCMS/wwwroot/screenshots/appoitment%20detail.PNG)

## 📹 Demo Video

Watch the demo on YouTube: [Clinic Management System Demo](https://www.youtube.com/watch?v=YOUR_VIDEO_ID)


[![Watch the demo](https://github.com/majedkaraali/WebCMS/blob/d4ab6ffc6f9319832c9926c0793bcfd3a6b1ecf3/WebCMS/wwwroot/screenshots/thmb.png)](https://youtu.be/eOQbz8T58IA)



## 📌 Project Overview  

### 🎯 Project Goal  

The primary objective of the **Clinic Management System** is to provide an all-in-one web application that enables:  

- ✅ Patient self-registration and appointment booking  
- ✅ Secure electronic medical record (EMR) storage and access  
- ✅ Efficient doctor-patient interaction through online consultations  
- ✅ Lab test ordering and result management  
- ✅ Administrative control over clinic workflow and staff roles  

## 🔑 Core Features  

### 👩‍⚕️ Patient Module  
- Register and manage personal profile  
- Book, reschedule, or cancel appointments  
- View assigned diseases, prescriptions, and lab results  

### 🩺 Doctor Module  
- View upcoming appointments  
- Access and edit patient medical records (diagnosis, lab orders, prescriptions)  
- Manage availability and personal profile  

### 🧪 Lab Module  
- Receive lab orders from doctors  
- Manage test categories and input results  
- Print test reports  

### 📋 Administrative Staff Module  
- Register and manage patient, doctor, and lab worker accounts  
- Control user roles using **Identity Framework**  
- Oversee clinic operations and appointment schedules  


## 🛠️ Technology Stack  

- **Frontend**: HTML5, CSS, JavaScript  
- **Backend**: ASP.NET Core MVC 8.0 (C#)  
- **Database**: SQL Server  
- **ORM & Authentication**: Entity Framework Core, Identity Framework  



## 🚀 Installation & Setup

1. **Clone the repository**:
```bash
git clone https://github.com/majedkaraali/WebCMS.git
cd WebCMS
````

2. **Download and restore the database**

- Download the database backup file (`.bak`) from [this link](https://drive.google.com/file/d/1UOH2D8A_Bfdpl3VGWk9QNQpg20CBDhYt/view?usp=drive_link).

- **Restore the database using SQL Server Management Studio (SSMS):**  
  1. Open SSMS and connect to your SQL Server instance.  
  2. Right-click on **Databases** → **Restore Database**.  
  3. Choose **Device** → select the downloaded `.bak` file.  
  4. Follow the prompts to restore the database.  

3. **Update the connection string** in `appsettings.json` to point to the restored database.  

4. **Open the project** in your preferred IDE (Visual Studio / Rider).

5. **Run database migrations**:

```bash
dotnet ef database update
```

6. **Start the application**:

```bash
dotnet run
```


## ▶️ Usage

- **Login as Patient** → Book and manage appointments, view prescriptions and lab results  
- **Login as Doctor** → Manage patients, consultations, diagnoses, prescriptions, and lab orders  
- **Login as Lab Worker** → Process and upload lab test results  
- **Login as Admin Staff** → Manage users, roles, and clinic operations  






## 🤝 Contributing

Contributions are welcome!  

1. Fork the repo  
2. Create a feature branch:
```bash
git checkout -b feature/your-feature
````

3. Commit your changes:

```bash
git commit -m "Add some feature"
```

4. Push to the branch:

```bash
git push origin feature/your-feature
```

5. Open a Pull Request

---

## 📜 License

This project is licensed under the **MIT License** – feel free to use, modify, and distribute.






