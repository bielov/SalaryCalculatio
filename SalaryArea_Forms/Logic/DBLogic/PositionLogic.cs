using SalaryArea3._2.Context;
using SalaryArea3._2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SalaryArea_Forms.Logic
{
    public class PositionLogic
    {   

        SalDbContext _dbContext = null;
        public PositionLogic()
        {
            _dbContext = new SalDbContext();
        }
        internal IEnumerable<Position> Get()
        {
            return _dbContext.Positions.ToList();
         
        }
        
        internal bool Exіstence (Position pos)
        {
            var emplwithpos = _dbContext.Employees.FirstOrDefault(p => p.PositionID == pos.PositionId);
            if(emplwithpos == null) { return true; }
            else
            {
                MessageBox.Show("Дана посада вже прив'язана до працівника.\n "+
                    "Будь ласка оновіть чи видаліть співробітників, які мають зв'зяок з даною посадою",
                    "Зауваження");
                return false;
            }
        }
        internal void Add(Position pos)
        {
            if (CheckValidation(pos) ==true)
            {
                try
                {
                    using (SalDbContext _dbContext = new SalDbContext())
                    {
                        _dbContext.Positions.Add(pos);
                        _dbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show( ex.ToString(),"Erro");
                }
                MessageBox.Show("Професія збережена");
            }
            
        }
     
        internal void Delete(Position pos)
        {
            if (MessageBox.Show("Ви впевнені, що бажаєте виділити дану позицію?",
                "Підтвердіть рішення.", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (pos.Employees.Count() == 0)
                {
                    try
                    {
                        _dbContext.Positions.Remove(pos);
                        _dbContext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception: {0}", ex.ToString());
                    }
                    MessageBox.Show("Професія видалена");
                }
                else
                {
                    MessageBox.Show("Сутність не може бути видаленою, так як вже має привязку до співробітника.\n"
                        + "Обновіть чи видаліть співробітника для видалення даного обєкту");
                }
            }
        }
        internal void Update(Position pos)
        {
            var updatedpos = _dbContext.Positions.FirstOrDefault(p => p.PositionId == pos.PositionId);
            updatedpos.PositionName = pos.PositionName;

            try
            {
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: {0}", ex.ToString());
            }
            MessageBox.Show("Професія оновлена");
        }
    
        private bool CheckValidation(Position pos)
        {
            if (string.IsNullOrWhiteSpace(pos.PositionName))
            {
                MessageBox.Show("Поле не може бути пустим");
                return false;
            }
            using (SalDbContext _dbContext = new SalDbContext())
            {
                var position = _dbContext.Positions
                 .FirstOrDefault(p => p.PositionName == pos.PositionName);
                if (position == null) { return true; }
                else
                {
                    MessageBox.Show("Професія {0} є базі даних ", pos.PositionName);
                    return false;
                }
            }

        }

        }
}
