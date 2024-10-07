using AutoMapper;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.DTOs;
using Karma.Application.Extensions;
using Karma.Application.Helpers;
using Karma.Application.Services.Interfaces;
using Karma.Core.EntityBuilders;
using Karma.Core.Repositories.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Stimulsoft.Base;
using Stimulsoft.Report;

namespace Karma.Application.Services
{
    public class ResumeReadService : IResumeReadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ResumeReadService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<AboutMeDTO> GetAboutMeAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return new AboutMeDTO()
            {
                MainJobTitle = resume.MainJobTitle,
                Description = resume.Description,
                ImageId = user.ImageId,
                SocialMedias = _mapper.Map<IEnumerable<SocialMediaDTO>>(resume.SocialMedias)
            };
        }

        public async Task<BasicInfoDTO> GetBasicInfoAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            return _mapper.Map<BasicInfoDTO>(user);
        }

        public async Task<IEnumerable<CareerRecordDTO>> GetCareerRecordsAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<CareerRecordDTO>>(resume.CareerRecords);
        }

        public async Task<IEnumerable<EducationalRecordDTO>> GetEducationalRecordsAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<EducationalRecordDTO>>(resume.EducationalRecords);
        }

        public async Task<IEnumerable<LanguageDTO>> GetLanguagesAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<LanguageDTO>>(resume.Languages);
        }

        public async Task<IEnumerable<SoftwareSkillDTO>> GetSoftwareSkillsAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<SoftwareSkillDTO>>(resume.SoftwareSkills);
        }

        public async Task<IEnumerable<AdditionalSkillDTO>> GetAdditionalSkillsAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<AdditionalSkillDTO>>(resume.AdditionalSkills);
        }

        public async Task<IEnumerable<ResumeQueryDTO>> GetResumesAsync(PageQuery pageQuery, ResumeFilterCommand command)
        {
            var resumes = await _unitOfWork.ExpandedResumeViewRepository.GetExpandedResumes();

            var filtredResumes = new ResumeQueryBuilder(resumes)
                .WithCareerExperienceLength(command.CareerExperienceLength)
                .WithCity(command.City)
                .WithMilitaryServiceStatuses(command.MilitaryServiceStatuses)
                .WithMaritalStatus(command.MaritalStatus)
                .WithCode(command.Code)
                .WithJobTitle(command.JobTitle)
                .WithGender(command.Gender)
                .WithDegreeLevels(command.DegreeLevels)
                .WithJobCategories(command.JobCategoryIds)
                .WithLanguages(command.LanguageIds)
                .WithOlderThan(command.OlderThan)
                .WithYoungerThan(command.YoungerThan)
                .WithSoftwareSkill(command.SoftwareSkillIds)
                .Build();

            return _mapper.Map<IEnumerable<ResumeQueryDTO>>(filtredResumes)
                .OrderByDescending(c => c.DegreeLevel)
                .DistinctBy(c => c.Id).ToPagingAndSorting(pageQuery);
        }

        public async Task<(FileStream stream, string filename)> DownloadPersonalResumeAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            if (resume.ResumeFileId is null)
                throw new ManagedException("رزومه شخصی بارگذاری نشده است.");

            return await _fileService.GetFileAsync(resume.ResumeFileId.Value);

        }

        public async Task<IEnumerable<WorkSampleDTO>> GetWorkSamplesAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<WorkSampleDTO>>(resume.WorkSamples);
        }

        public async Task<UserResumeDTO> GetUserResumeAsync(Guid id)
        {
            var resume = await _unitOfWork.ResumeRepository.GetByIdAsync(id) ??
                throw new ManagedException("رزومه کاربر مورد نظر یافت نشد.");

            return _mapper.Map<UserResumeDTO>(resume);
        }

        public async Task<(MemoryStream file, string name)> DownloadKarmaResumeAsync(Guid userId)
        {
            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User.Id == userId) ??
                throw new ManagedException("رزومه شما یافت نشد.");

            var socialMedias = _mapper.Map<IEnumerable<SocialMediaDTO>>(resume.SocialMedias);
            var careerRecords = _mapper.Map<IEnumerable<CareerRecordDTO>>(resume.CareerRecords);
            var educationalRecords = _mapper.Map<IEnumerable<EducationalRecordDTO>>(resume.EducationalRecords);
            var languages = _mapper.Map<IEnumerable<LanguageDTO>>(resume.Languages);
            var softwareSkills = _mapper.Map<IEnumerable<SoftwareSkillDTO>>(resume.SoftwareSkills);
            var workSamples = _mapper.Map<IEnumerable<WorkSampleDTO>>(resume.WorkSamples);
            var additionalSkills = _mapper.Map<IEnumerable<AdditionalSkillDTO>>(resume.AdditionalSkills);


            FileStream? stream = null;
            byte[]? logoFileBytes = null;

            try
            {

                if (resume.User.ImageId is not null)
                {
                    stream = (await _fileService.GetFileAsync(resume.User.ImageId.Value)).stream;
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        logoFileBytes = memoryStream.ToArray();
                    }
                }
            }
            catch (Exception)
            {
            }

            var resumeToPrint = new ResumePrintDTO()
            {
                LogoFile = logoFileBytes,
                PhoneNumber = resume.User.PhoneNumber!,
                Logo = resume.User.ImageId,
                MainJobTitle = resume.MainJobTitle,
                Description = resume.Description,
                AdditionalSkills = additionalSkills,
                Email = resume.User.Email,
                Code = resume.Code,
                FirstName = resume.User.FirstName,
                LastName = resume.User.LastName,
                MilitaryServiceStatus = resume.User.MilitaryServiceStatus.GetDescription(),
                BirthDate = resume.User.BirthDate,
                CareerRecords = careerRecords,
                City = resume.User.City,
                EducationalRecords = educationalRecords,
                Languages = languages,
                SoftwareSkills = softwareSkills,
                WorkSamples = workSamples,
                Gender = resume.User.Gender?.GetDescription(),
                MaritalStatus = resume.User.MaritalStatus?.GetDescription(),
                SocialMedias = socialMedias
            };


            Console.WriteLine(Directory.GetCurrentDirectory() + "/StaticFiles/data.json");

            using (var writer = new StreamWriter(Directory.GetCurrentDirectory() + "/StaticFiles/data.json"))
            {
                writer.WriteLine(JsonConvert.SerializeObject(resumeToPrint, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
            }


            var arialFontPath = Directory.GetCurrentDirectory() + "/StaticFiles/arial.ttf";
            var iranSansFontPath = Directory.GetCurrentDirectory() + "/StaticFiles/Iranian Sans.ttf";
            var bNazaninfontPath = Directory.GetCurrentDirectory() + "/StaticFiles/B-NAZANIN.TTF";
            var filePath = Directory.GetCurrentDirectory() + "/StaticFiles/Report.mrt";
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            lines[9] = $"        <PathData>{Directory.GetCurrentDirectory() + "/StaticFiles/data.json"}</PathData>";
            File.WriteAllLines(filePath, lines);

            Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHkO46nMQvol4ASeg91in+mGJLnn2KMIpg3eSXQSgaFOm15+0l" +
            "hekKip+wRGMwXsKpHAkTvorOFqnpF9rchcYoxHXtjNDLiDHZGTIWq6D/2q4k/eiJm9fV6FdaJIUbWGS3whFWRLPHWC" +
            "BsWnalqTdZlP9knjaWclfjmUKf2Ksc5btMD6pmR7ZHQfHXfdgYK7tLR1rqtxYxBzOPq3LIBvd3spkQhKb07LTZQoyQ" +
            "3vmRSMALmJSS6ovIS59XPS+oSm8wgvuRFqE1im111GROa7Ww3tNJTA45lkbXX+SocdwXvEZyaaq61Uc1dBg+4uFRxv" +
            "yRWvX5WDmJz1X0VLIbHpcIjdEDJUvVAN7Z+FW5xKsV5ySPs8aegsY9ndn4DmoZ1kWvzUaz+E1mxMbOd3tyaNnmVhPZ" +
            "eIBILmKJGN0BwnnI5fu6JHMM/9QR2tMO1Z4pIwae4P92gKBrt0MqhvnU1Q6kIaPPuG2XBIvAWykVeH2a9EP6064e11" +
            "PFCBX4gEpJ3XFD0peE5+ddZh+h495qUc1H2B";

            var report = new StiReport();

            StiFontCollection.AddFontFile(arialFontPath);
            StiFontCollection.AddFontFile(iranSansFontPath);
            StiFontCollection.AddFontFile(bNazaninfontPath);

            report.Load(filePath);
            report.Render();

            var reportStream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, reportStream);
            reportStream.Position = 0;

            return (reportStream, resume.User.FirstName is not null && resume.User.LastName is not null ?
                $"{resume.User.FirstName} {resume.User.LastName}.pdf" : "cv.pdf");
        }
    }
}
