﻿namespace Corp.ERP.Inventory.Application.Contract.Dto;

public class UserDto
{
    public virtual Guid Id { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual string FullName { get; set; }
    public virtual string Email { get; set; }

    public static implicit operator UserDto(User _model)
    {
        if (_model == null) 
            return null;

        return new UserDto
        {
            Id = _model.Id,
            FirstName = _model.FirstName,
            LastName = _model.LastName,
            FullName = _model.FullName,
            Email = _model.Email,
        };
    }

    public static implicit operator User(UserDto _model)
    {
        if (_model == null)
            return null;

        return new User
        {
            Id = _model.Id,
            FirstName = _model.FirstName,
            LastName = _model.LastName,
            //FullName = _model.FullName,
            Email = _model.Email,
        };
    }
}