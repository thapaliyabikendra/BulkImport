﻿using AutoMapper;
using CommonLibs.BulkImport.Sample.Dtos;
using CommonLibs.BulkImport.Sample.Entities;

namespace CommonLibs.BulkImport.Sample;

public class SampleApplicationAutoMapperProfile : Profile
{
    public SampleApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<StudentDto, Student>();
    }
}
