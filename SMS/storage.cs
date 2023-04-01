using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS
{
    //class for stroing all the values of usernmae,password,and role all variable can from any class

    class storage
    {
        public static string username;
        public static string password;
        public static string role;
        //bool for a add,update,delete,view for a staff
        public static bool addStaff;
        public static bool updateStaff;
        public static bool deleteStaff;
        public static bool viewStaff;
        //bool for a add,update,delete,view for a product
        public static bool addProduct;
        public static bool updateProduct;
        public static bool deleteProduct;
        public static bool viewProduct;
        //bool for a add,update,delete,view for a category
        public static bool addCategory;
        public static bool updateCategory;
        public static bool deleteCategory;
        public static bool viewCategory;

        

        public static string getUsername()
        {
            return username;
        }

        public static string getPassword()
        {
            return password;
        }

        public static string getRole()
        {
            return role;
        }

        public static bool getAddStaff()
        {
            return addStaff;
        }

        public static bool getUpdateStaff()
        {
            return updateStaff;
        }

        public static bool getDeleteStaff()
        {
            return deleteStaff;
        }

        public static bool getViewStaff()
        {
            return viewStaff;
        }

        public static bool getAddProduct()
        {
            return addProduct;
        }

        public static bool getUpdateProduct()
        {
            return updateProduct;
        }

        public static bool getDeleteProduct()
        {
            return deleteProduct;
        }

        public static bool getViewProduct()
        {
            return viewProduct;
        }


        public static void setUsername(string username)
        {
            storage.username = username;
        }

        public static void setPassword(string password)
        {
            storage.password = password;
        }

        public static void setRole(string role)
        {
            storage.role = role;
        }

        public static void setAddStaff(bool addStaff)
        {
            storage.addStaff = addStaff;
        }

        public static void setUpdateStaff(bool updateStaff)
        {
            storage.updateStaff = updateStaff;
        }

        public static void setDeleteStaff(bool deleteStaff)
        {
            storage.deleteStaff = deleteStaff;
        }

        public static void setViewStaff(bool viewStaff)
        {
            storage.viewStaff = viewStaff;
        }

        public static void setAddProduct(bool addProduct)
        {
            storage.addProduct = addProduct;
        }

        public static void setUpdateProduct(bool updateProduct)
        {
            storage.updateProduct = updateProduct;
        }

        public static void setDeleteProduct(bool deleteProduct)
        {
            storage.deleteProduct = deleteProduct;
        }

        public static void setViewProduct(bool viewProduct)
        {
            storage.viewProduct = viewProduct;
        }

    }

}