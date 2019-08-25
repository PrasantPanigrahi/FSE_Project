using AutoMapper;
using PM.DAL.Repositories.Interface;
using PM.Extensions.DTO;
using PM.Extensions.Interfaces;
using PM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PM.Extensions
{
    public class ParentTaskFacade : IParentTaskFacade
    {
        private readonly IParentTaskRepository _taskRepository;

        public ParentTaskFacade(IParentTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// get parent task
        /// </summary>
        /// <param name="id">task id</param>
        /// <returns>task for the given id</returns>
        public ParentTaskDto Get(int id)
        {
            var task = _taskRepository.Get(id);
            if (task == null)
            {
                throw new InvalidOperationException("task does not exists");
            }

            var taskDto = Mapper.Map<ParentTaskDto>(task);
            return taskDto;
        }

        /// <summary>
        /// Get all parent tasks
        /// </summary>
        /// <returns>tasks list</returns>
        public List<ParentTaskDto> GetAll()
        {
            var tasks = _taskRepository.GetAll()
                        .OrderByDescending(p => p.Id);
            var taskDtos = Mapper.Map<List<ParentTaskDto>>(tasks);

            return taskDtos;
        }

        /// <summary>
        /// either create or update provided task
        /// </summary>
        /// <param name="taskDto"></param>
        /// <returns></returns>
        public ParentTaskDto Update(ParentTaskDto taskDto)
        {
            var task = _taskRepository.Get(taskDto.Id);
            if (task == null)
            {
                //create Task
                task = Mapper.Map<ParentTask>(taskDto);
                task.Id = 0;
                _taskRepository.Add(task);
            }
            else
            {
                //update task
                task.Name = taskDto.Name;
            }
            _taskRepository.SaveChanges();

            return taskDto;
        }
    }
}