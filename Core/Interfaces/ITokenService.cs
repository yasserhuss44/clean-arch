﻿using Core.Models;

namespace Core.Interfaces;

public interface ITokenService
{
    string GetToken(User user);
}
