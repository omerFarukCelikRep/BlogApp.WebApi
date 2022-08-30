import React from 'react';
import Header from './Header';
import '../../assets/css/style.css';
import SideBar from './SideBar';

const Layout = () => {
  return (
    <div className='container'>
      <Header />
      <SideBar />
    </div>
  )
}

export default Layout;