﻿namespace Models.Interfaces
{
    public class ICouple
    {
        int Id { get; set; }
        int DoctorId { get; set; }
        int PatientId { get; set; }
    }
}
