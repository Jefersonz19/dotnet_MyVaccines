﻿namespace MyVaccine.WebaApi.Dtos;

public class AuthResponseDto
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public  bool IsSuccess { get; set; }
    public string[] Errors { get; set; }
}
