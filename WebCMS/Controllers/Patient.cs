﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebCMS.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace WebCMS.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.FirstOrDefault(p => p.UserId == userId);


            var appointments = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.PatientId == patient.Id)
                .OrderBy(a => a.AppointmentDate)
                .ToList();

            ViewBag.Patient = patient;

            return View(appointments);
        }

        public IActionResult Book()
        {
            ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public IActionResult Book(Appointment appointment)
        {
           

            // Get the logged-in patient ID
            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (patientId == null)
            {
                return Unauthorized(); // Ensure user is logged in
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.FirstOrDefault(p => p.UserId == userId);

            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            appointment.PatientId = patient.Id; // Assign the integer PatientId

            appointment.Status = "Scheduled"; // Default status

            // Check if the doctor is available at the selected time
            var isDoctorAvailable = !_context.Appointments
                .Any(a => a.DoctorId == appointment.DoctorId &&
                          a.AppointmentDate == appointment.AppointmentDate);

            if (!isDoctorAvailable)
            {
                ModelState.AddModelError("", "The selected doctor is not available at this time.");
                ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "FullName");
                return View(appointment);
            }

            // Save the appointment
            _context.Appointments.Add(appointment);
            _context.SaveChanges();


            TempData["SuccessMessage"] = "Appointment booked successfully!";
            return RedirectToAction("Index");
        }

    
        public IActionResult Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.FirstOrDefault(p => p.UserId == userId);

            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            return View(patient);

    
        }

        public IActionResult Cancel(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound(); // Handle case where appointment doesn't exist
            }
            appointment.IsCancelled = true;
            appointment.UpdatedDate = DateTime.Now;
            appointment.Status = "Cancelled";
            _context.SaveChanges();



            return RedirectToAction("Index");
        }


      

        [HttpPost]
        public IActionResult Update(Patient updatedPatient)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.FirstOrDefault(p => p.UserId == userId);

            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            // Update patient details  
            patient.FullName = updatedPatient.FullName;
            patient.Email = updatedPatient.Email;
            patient.Address = updatedPatient.Address;
            patient.PhoneNumber = updatedPatient.PhoneNumber;
            patient.DateOfBirth = updatedPatient.DateOfBirth;
            patient.Gender = updatedPatient.Gender;
            patient.EmergencyContactName = updatedPatient.EmergencyContactName;
            patient.EmergencyContactPhone = updatedPatient.EmergencyContactPhone;
            patient.InsuranceProvider = updatedPatient.InsuranceProvider;
            patient.PolicyNumber = updatedPatient.PolicyNumber;
            patient.Occupation = updatedPatient.Occupation;
            patient.Allergies = updatedPatient.Allergies;
            patient.SocialHistory = updatedPatient.SocialHistory;

            _context.SaveChanges(); 

            return RedirectToAction("Index");   
        }



        public async Task<IActionResult> Logout()
        {

            return RedirectToAction("Logout", "Account"); // Redirect to login page after logout
        }


    }
}
